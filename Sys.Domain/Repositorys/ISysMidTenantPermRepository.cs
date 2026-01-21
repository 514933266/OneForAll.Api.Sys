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
    /// 租户权限
    /// </summary>
    public interface ISysMidTenantPermRepository : IEFCoreRepository<SysMidTenantPermission>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysMidTenantPermission>> GetListByTenantAsync(Guid tenantId);
    }
}
