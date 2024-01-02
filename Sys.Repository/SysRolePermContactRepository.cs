using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class SysRolePermContactRepository : Repository<SysRolePermContact>, ISysRolePermContactRepository
    {
        public SysRolePermContactRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListPermissionAsync(Guid id)
        {
            var permDbSet = Context.Set<SysPermission>();
            return await (from role in DbSet.Where(w => w.SysRoleId == id)
                          join perm in permDbSet on role.SysPermissionId equals perm.Id
                          select perm).ToListAsync();
        }

        /// <summary>
        /// 查询租户默认角色权限列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysRolePermContact>> GetListByTenantAsync(Guid tenantId)
        {
            var roleDbSet = Context.Set<SysRole>().Where(w => w.SysTenantId == tenantId);
            return await (from role in roleDbSet
                          join contact in DbSet on role.Id equals contact.SysRoleId
                          select contact).ToListAsync();
        }
    }
}
