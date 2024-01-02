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
    /// 角色用户
    /// </summary>
    public interface ISysRoleUserContactRepository : IEFCoreRepository<SysRoleUserContact>
    {
        /// <summary>
        /// 查询角色用户
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysUser>> GetListUserAsync(Guid id);

        /// <summary>
        /// 查询租户角色用户列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysRoleUserContact>> GetListByTenantAsync(Guid tenantId);
    }
}

