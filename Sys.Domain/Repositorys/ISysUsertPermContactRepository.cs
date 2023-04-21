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
    /// 用户权限
    /// </summary>
    public interface ISysUsertPermContactRepository : IEFCoreRepository<SysUserPermContact>
    {
        /// <summary>
        /// 查询租户默认用户权限列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysUserPermContact>> GetListByTenantAsync(Guid tenantId);
    }
}
