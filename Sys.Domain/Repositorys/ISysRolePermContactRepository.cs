using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public interface ISysRolePermContactRepository : IEFCoreRepository<SysRolePermContact>
    {
        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysPermission>> GetListPermissionAsync(Guid id);

        /// <summary>
        /// 查询租户默认角色权限列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysRolePermContact>> GetListByTenantAsync(Guid tenantId);
    }
}
