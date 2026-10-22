using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 权限查询服务（只读）
    /// </summary>
    public interface ISysPermissionQueryService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysPermissionAggr>> GetPageAsync(int pageIndex, int pageSize, string key, Guid menuId);

        /// <summary>
        /// 获取菜单下的权限列表
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysPermission>> GetListByMenuAsync(Guid menuId);
    }
}
