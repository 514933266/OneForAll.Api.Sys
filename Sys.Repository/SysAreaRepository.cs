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
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 地区
    /// </summary>
    public class SysAreaRepository : Repository<SysArea>, ISysAreaRepository
    {
        public SysAreaRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysArea>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId)
        {
            var predicate = PredicateBuilder.Create<SysArea>(w => true);
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Name.Contains(key));
            if (parentId >= 0) predicate = predicate.And(w => w.ParentId == parentId);

            var total = await DbSet
               .CountAsync(predicate);

            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<SysArea>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<SysArea>> GetListAsync(IEnumerable<int> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns>实体</returns>
        public async Task<SysArea> GetByCodeAsync(string code)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.Code.Equals(code));
        }

        /// <summary>
        /// 查询子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysArea>> GetChildrenAsync(int parentId)
        {
            return await DbSet.Where(w => w.ParentId == parentId).ToListAsync();
        }
    }
}
