using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Repository
{
    /// <summary>
    /// 租户服务设置
    /// </summary>
    public class SysTenantServiceSettingRepository : Repository<SysTenantServiceSetting>, ISysTenantServiceSettingRepository
    {
        public SysTenantServiceSettingRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询服务已到期租户
        /// </summary>
        /// <returns>结果</returns>
        public async Task<IEnumerable<SysTenantServiceSetting>> GetListExpiryAsync()
        {
            var tenantDbSet = Context.Set<SysTenant>().Where(w => w.IsEnabled);

            var sql = (from setting in DbSet
                       join tenant in tenantDbSet on setting.SysTenantId equals tenant.Id
                       where setting.EndTime < DateTime.Now
                       select setting);
            return await sql.ToListAsync();
        }
    }
}
