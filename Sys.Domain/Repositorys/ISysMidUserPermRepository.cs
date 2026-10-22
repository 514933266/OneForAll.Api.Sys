using OneForAll.EFCore;
using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public interface ISysMidUserPermRepository : IEFCoreRepository<SysMidUserPermission>
    {
        /// <summary>
        /// 查询租户默认用户权限列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysMidUserPermission>> GetListByTenantAsync(Guid tenantId);

        /// <summary>
        /// 查询用户权限列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysMidUserPermission>> GetListByUserAsync(Guid userId);
    }
}
