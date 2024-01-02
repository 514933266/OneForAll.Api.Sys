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
        public async Task<BaseErrType> AddAsync(SysUserForm form)
        {
            if (form.Password != form.RePassword) return BaseErrType.DataNotMatch;
            var data = await _userRepository.GetAsync(form.UserName);
            if (data != null) return BaseErrType.DataExist;
            var tenant = await _tenantRepository.FindAsync(form.TenantId);
            if (tenant == null) return BaseErrType.DataError;


            data = _mapper.Map<SysUserForm, SysUser>(form);
            if (data.Password.IsNullOrEmpty())
                data.Password = data.UserName.ToMd5();
            if (form.UserName.IsMobile() && form.Mobile.IsNullOrEmpty())
                form.Mobile = form.UserName;
            return await ResultAsync(() => _userRepository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysUserUpdateForm form)
        {
            var data = await _userRepository.GetAsync(form.UserName);
            if (data != null)
            {
                if (data.Id != form.Id)
                    return BaseErrType.DataExist;
            }
            else
            {
                data = await _userRepository.FindAsync(form.Id);
                if (data == null)
                    return BaseErrType.DataNotFound;
            }

            if (data.IsDefault && !form.IsDefault)
            {
                // 取消默认账号时清空个人权限
                var perms = await _userPermRepository.GetListAsync(w => w.SysUserId == data.Id);
                await _userPermRepository.DeleteRangeAsync(perms);
            }
            _mapper.Map(form, data);
            return await ResultAsync(_userRepository.SaveChangesAsync);
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
