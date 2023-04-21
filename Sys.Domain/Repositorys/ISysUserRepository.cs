using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

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
        /// 查询列表（默认账号）
        /// </summary>
        /// <param name="tenantId">用户id</param>
        /// <returns>用户</returns>
        Task<IEnumerable<SysUser>> GetListDefaultByTenantAsync(Guid tenantId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        Task<SysUser> GetAsync(string username);
    }
}
