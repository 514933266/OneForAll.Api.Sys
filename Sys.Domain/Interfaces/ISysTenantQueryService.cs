using OneForAll.Core;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 租户查询服务（只读，返回实体/聚合根，不含 DTO）
    /// </summary>
    public interface ISysTenantQueryService
    {
        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        Task<SysTenant> GetAsync(Guid id);

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        Task<PageList<SysTenant>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 按id集合获取租户
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysTenant>> GetListByIdsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 按关键字获取租户（下拉选项）
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysTenant>> GetListByKeyAsync(string key);

        /// <summary>
        /// 获取可分配的菜单权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>菜单权限列表</returns>
        Task<IEnumerable<SysMenuPermissionAggr>> GetListMenuAsync(Guid id);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysPermission>> GetListPermissionAsync(Guid id);
    }
}
