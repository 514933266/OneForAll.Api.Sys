using OneForAll.Core;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 用户查询服务（只读，返回聚合根/实体，不含 DTO）
    /// </summary>
    public class SysUserQueryService : ISysUserQueryService
    {
        private readonly ISysUserRepository _repository;

        public SysUserQueryService(ISysUserRepository repository)
        {
            _repository = repository;
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
            return await _repository.GetPageWithTenantAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 按id集合获取用户
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _repository.GetListAsync(ids);
        }

        /// <summary>
        /// 按租户与手机号获取用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>用户</returns>
        public async Task<SysUser> GetByMobileAsync(Guid tenantId, string mobile)
        {
            return await _repository.GetByTenantAndMobileAsync(tenantId, mobile);
        }

        /// <summary>
        /// 分页获取全部用户
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetPageAllAsync(int pageIndex, int pageSize)
        {
            return await _repository.GetPageAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 分页获取主账号用户
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetPageMainAccountAsync(int pageIndex, int pageSize)
        {
            return await _repository.GetPageMainAccountAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 按id获取用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户</returns>
        public async Task<SysUser> GetByIdAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        /// <summary>
        /// 按id与租户获取用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="tenantId">机构id</param>
        /// <returns>用户</returns>
        public async Task<SysUser> GetByIdAndTenantAsync(Guid id, Guid tenantId)
        {
            return await _repository.GetByTenantAndIdAsync(tenantId, id);
        }
    }
}
