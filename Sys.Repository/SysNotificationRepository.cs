using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public class SysNotificationRepository : Repository<SysNotification>, ISysNotificationRepository
    {
        public SysNotificationRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysNotification>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var predicate = PredicateBuilder.Create<SysNotification>(w => true);
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Title.Contains(key) || w.Title.Contains(key));
            if (startDate != null) predicate = predicate.And(w => w.CreateTime >= startDate);
            if (endDate != null) predicate = predicate.And(w => w.CreateTime <= endDate);

            var total = await DbSet.CountAsync(predicate);
            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<SysNotification>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysNotification>> GetListAsTrackingAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
        }
    }
}
