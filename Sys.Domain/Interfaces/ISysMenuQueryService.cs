using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 菜单查询服务（只读）
    /// </summary>
    public interface ISysMenuQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysMenuPermissionAggr>> GetListAsync(bool hasPerms, Guid parentId, string key);
    }
}
