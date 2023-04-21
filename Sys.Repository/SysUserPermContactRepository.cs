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
    /// 用户权限
    /// </summary>
    public class SysUserPermContactRepository : Repository<SysUserPermContact>, ISysUsertPermContactRepository
    {
        public SysUserPermContactRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询租户默认用户权限列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysUserPermContact>> GetListByTenantAsync(Guid tenantId)
        {
            var userDbSet = Context.Set<SysUser>().Where(w => w.SysTenantId == tenantId && w.IsDefault);
            return await (from user in userDbSet
                          join contact in DbSet on user.Id equals contact.SysUserId
                          select contact).ToListAsync();
        }
    }
}
