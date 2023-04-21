using Sys.Domain;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;

namespace Sys.Repository
{
    /// <summary>
    /// 租户
    /// </summary>
    public class SysTenantRepository : Repository<SysTenant>, ISysTenantRepository
    {
        public SysTenantRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        public async Task<PageList<SysTenant>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate)
        {
            var predicate = PredicateBuilder.Create<SysTenant>(w => true);
            if (isEnabled == SysEnabledEnum.Disabled)
                predicate = predicate.And(w => w.IsEnabled == false);
            else if (isEnabled == SysEnabledEnum.Enabled)
                predicate = predicate.And(w => w.IsEnabled == true);

            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Name.Contains(key));
            if (startDate != null) predicate = predicate.And(w => w.CreateTime >= startDate);
            if (endDate != null)
            {
                var date = endDate.Value.AddDays(1);
                predicate = predicate.And(w => w.CreateTime <= date);
            }

            var total = await DbSet
                .CountAsync(predicate);

            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(e => e.CreateTime)
                .ToListAsync();

            return new PageList<SysTenant>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysTenant>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询租户
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>结果</returns>
        public async Task<SysTenant> GetByNameAsync(string name)
        {
            return await DbSet
                .Where(w => w.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询租户
        /// </summary>
        /// <param name="code">租户信用代码</param>
        /// <returns>结果</returns>
        public async Task<SysTenant> GetByCodeAsync(string code)
        {
            return await DbSet.Where(w => w.Code.Equals(code)).FirstOrDefaultAsync();
        }
    }
}