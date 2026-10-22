using Sys.Domain.Entities;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;
using System.Linq;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface ISysUserRepository : IEFCoreRepository<SysUser>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        Task<PageList<SysUserAggr>> GetPageWithTenantAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询分页（主账号）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetPageMainAccountAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询默认账号列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>用户</returns>
        Task<IEnumerable<SysUser>> GetListDefaultByTenantAsync(Guid tenantId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>用户</returns>
        Task<IEnumerable<SysUser>> GetListByTenantAsync(Guid tenantId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        Task<SysUser> GetAsync(string username);

        /// <summary>
        /// 查询实体（手机号）
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByMobileAsync(string mobile);

        /// <summary>
        /// 查询实体（租户+手机号）
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="mobile">手机号</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByTenantAndMobileAsync(Guid tenantId, string mobile);

        /// <summary>
        /// 查询实体（租户+id）
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="id">用户id</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByTenantAndIdAsync(Guid tenantId, Guid id);

        /// <summary>
        /// 查询未包含指定id的用户列表
        /// </summary>
        /// <param name="ids">排除的用户id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetListNotInIdsAsync(IEnumerable<Guid> ids);
    }
}
