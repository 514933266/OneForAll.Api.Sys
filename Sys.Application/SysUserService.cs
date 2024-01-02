using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 应用用户
    /// </summary>
    public class SysUserService: ISysUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysUserManager _manager;
        private readonly ISysUserRepository _repository;
        public SysUserService(
            IMapper mapper,
            ISysUserManager manager,
            ISysUserRepository repository)
        {
            _mapper = mapper;
            _manager = manager;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        public async Task<PageList<SysUserDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key);

            var items = _mapper.Map<IEnumerable<SysUserAggr>, IEnumerable<SysUserDto>>(data.Items);
            return new PageList<SysUserDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<SysUserDto>> GetListAsync(IEnumerable<Guid> ids)
        {
            if (ids.Any())
            {
                var data = await _repository.GetListAsync(w => ids.Contains(w.Id)) as List<SysUser>;
                return _mapper.Map<IEnumerable<SysUser>, IEnumerable<SysUserDto>>(data);
            }
            return Enumerable.Empty<SysUserDto>();
        }

        /// <summary>
        /// 根据电话获取用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>用户列表</returns>
        public async Task<SysUserDto> GetByMobileAsync(Guid tenantId, string mobile)
        {
            var data = await _repository.GetAsync(w => w.SysTenantId == tenantId && w.Mobile == mobile);
            return _mapper.Map<SysUserDto>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysUserForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysUserUpdateForm form)
        {
            return await _manager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }
    }
}
