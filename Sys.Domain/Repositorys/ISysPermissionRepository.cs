using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 权限
    /// </summary>
    public interface ISysPermissionRepository : IEFCoreRepository<SysPermission>
    {
        /// <summary>
        /// 查询分页（含菜单）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysPermissionAggr>> GetPageWithMenuAsync(int pageIndex, int pageSize, string key, Guid menuId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysPermission>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="menuIds">菜单id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<SysPermission>> GetListByMenuAsync(IEnumerable<Guid> menuIds);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<SysPermission>> GetListByMenuAsync(Guid menuId);

        /// <summary>
        /// 查询租户权限
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns></returns>
        Task<IEnumerable<SysPermission>> GetListByTenantAsync(Guid tenantId);

        /// <summary>
        /// 查询菜单查看权限
        /// </summary>
        /// <param name="menuIds">菜单id</param>
        /// <returns></returns>
        Task<IEnumerable<SysPermission>> GetListEnterViewByMenuAsync(IEnumerable<Guid> menuIds);

    }
}
