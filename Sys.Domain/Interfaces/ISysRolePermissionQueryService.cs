using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 角色权限查询服务（只读）
    /// </summary>
    public interface ISysRolePermissionQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysPermission>> GetListAsync(Guid roleId);
    }
}
