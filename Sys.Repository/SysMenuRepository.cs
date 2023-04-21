using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.ORM;
using OneForAll.Core.Extension;

namespace Sys.Repository
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysMenuRepository : Repository<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parentId">上级菜单id</param>
        /// <param name="key">关键字</param>
        /// <returns>实体</returns>
        public async Task<IEnumerable<SysMenu>> GetListAsync(Guid parentId, string key)
        {
            var predicate = PredicateBuilder.Create<SysMenu>(w => true);
            if (parentId != Guid.Empty)
                predicate = predicate.And(w => w.ParentId == parentId);
            if (!key.IsNullOrWhiteSpace())
                predicate = predicate.And(w => w.Name.Contains(key));

            return await DbSet.Where(predicate).OrderBy(o => o.SortNumber).ToListAsync();
        }

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysMenu>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .OrderBy(e => e.SortNumber)
                .ToListAsync();
        }
    }
}
