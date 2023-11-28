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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Repository
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class SysContactUsRepository : Repository<SysContactUs>, ISysContactUsRepository
    {
        public SysContactUsRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<SysContactUs>(w => true);
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.Contact.Contains(key) || w.Name.Contains(key));

            var total = await DbSet.CountAsync(predicate);
            var data = await DbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageList<SysContactUs>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysContactUs>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}
