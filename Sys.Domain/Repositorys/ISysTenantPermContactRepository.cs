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
    /// 租户权限
    /// </summary>
    public interface ISysTenantPermContactRepository : IEFCoreRepository<SysTenantPermContact>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysTenantPermContact>> GetListByTenantAsync(Guid tenantId);
    }
}
