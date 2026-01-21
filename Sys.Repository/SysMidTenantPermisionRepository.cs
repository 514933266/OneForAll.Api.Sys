using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 租户权限
    /// </summary>
    public class SysMidTenantPermisionRepository : Repository<SysMidTenantPermission>, ISysMidTenantPermRepository
    {
        public SysMidTenantPermisionRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysMidTenantPermission>> GetListByTenantAsync(Guid tenantId)
        {
            return await DbSet.Where(w => w.SysTenantId == tenantId).ToListAsync();
        }
    }
}
