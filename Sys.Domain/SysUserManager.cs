using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Security;
using Sys.Domain.Aggregates;

namespace Sys.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUserManager : BaseManager, ISysUserManager
    {
        private readonly IMapper _mapper;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysTenantRepository _tenantRepository;
        private readonly ISysUsertPermContactRepository _userPermRepository;

        public SysUserManager(
            IMapper mapper,
            ISysUserRepository userRepository,
            ISysTenantRepository tenantRepository,
            ISysUsertPermContactRepository userPermRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _userPermRepository = userPermRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        public async Task<PageList<SysUserAggr>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _userRepository.GetPageWithTenantAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseMessage> AddAsync(SysUserForm form)
        {
            var msg = new BaseMessage();

            #region 校验

            if (form.Password != form.RePassword)
                return msg.Fail(BaseErrType.DataNotMatch, "两次密码输入不一致");
            var tenant = await _tenantRepository.FindAsync(form.TenantId);
            if (tenant == null)
                return msg.Fail(BaseErrType.DataError, "请先选择租户");
            var data = await _userRepository.GetAsync(form.UserName);
            if (data != null)
            {
                msg.Data = data.Id;// 返回id以便部分业务场景使用
                return msg.Fail(BaseErrType.DataExist, "账号已存在");
            }
            #endregion

            data = _mapper.Map<SysUserForm, SysUser>(form);

            // 默认密码
            if (data.Password.IsNullOrEmpty())
                data.Password = data.UserName.ToMd5();

            // 默认手机
            if (data.UserName.IsMobile() && data.Mobile.IsNullOrEmpty())
                data.Mobile = data.UserName;

            // 检测手机号是否被使用
            if (!form.Mobile.IsNullOrEmpty())
            {
                var exists = await _userRepository.GetAsync(w => w.Mobile == form.Mobile);
                if (exists != null)
                {
                    msg.Data = exists.Id;
                    return msg.Fail(BaseErrType.DataExist, "手机号码已被使用");
                }
            }

            var effected = await _userRepository.AddAsync(data);
            msg.Data = data.Id;

            return effected > 0 ? msg.Success("添加账号成功") : msg.Fail("添加账号失败");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseMessage> UpdateAsync(SysUserUpdateForm form)
        {
            var msg = new BaseMessage();
            var data = await _userRepository.GetAsync(form.UserName);
            if (data != null && data.Id != form.Id)
                return msg.Fail(BaseErrType.DataExist, "账号已被使用");

            if (data == null)
                data = await _userRepository.FindAsync(form.Id);
            if (data == null)
                return msg.Fail(BaseErrType.DataError, "账号不存在");

            // 检测手机号是否被使用
            if (!form.Mobile.IsNullOrEmpty())
            {
                var exists = await _userRepository.GetAsync(w => w.Mobile == form.Mobile);
                if (exists != null && exists.Id != data.Id)
                    return msg.Fail(BaseErrType.DataExist, "手机号码已被使用");
            }

            // 取消默认账号时清空个人权限
            if (data.IsDefault && !form.IsDefault)
            {
                var perms = await _userPermRepository.GetListAsync(w => w.SysUserId == data.Id);
                await _userPermRepository.DeleteRangeAsync(perms);
            }

            _mapper.Map(form, data);
            var effected = await _userRepository.SaveChangesAsync();
            return effected > 0 ? msg.Success("修改账号成功") : msg.Fail("修改账号失败");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _userRepository.GetListAsync(ids);
            if (!data.Any())
                return BaseErrType.DataNotFound;

            return await ResultAsync(() => _userRepository.DeleteRangeAsync(data));
        }
    }
}
