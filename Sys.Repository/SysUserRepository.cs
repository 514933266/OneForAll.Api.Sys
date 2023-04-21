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
using Sys.Domain.Aggregates;

namespace Sys.Repository
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUserRepository : Repository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        public async Task<PageList<SysUserAggr>> GetPageWithTenantAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<SysUser>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(key) || w.UserName.Contains(key));
            }

            var dbSet = DbSet.Where(predicate);
            var tenantDbSet = Context.Set<SysTenant>();
            var total = await DbSet.CountAsync(predicate);

            var query = (from user in dbSet
                         join tenant in tenantDbSet on user.SysTenantId equals tenant.Id
                         select new SysUserAggr()
                         {
                             Id = user.Id,
                             Name = user.Name,
                             Status = user.Status,
                             IconUrl = user.IconUrl,
                             UserName = user.UserName,
                             IsDefault = user.IsDefault,
                             LastLoginIp = user.LastLoginIp,
                             LastLoginTime = user.LastLoginTime,
                             SysTenant = tenant
                         });

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<SysUserAggr>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询分页（主账号）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetPageMainAccountAsync(int pageIndex, int pageSize)
        {
            var predicate = PredicateBuilder.Create<SysUser>(w => w.IsDefault);

            return await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询列表（默认账号）
        /// </summary>
        /// <param name="tenantId">用户id</param>
        /// <returns>用户</returns>
        public async Task<IEnumerable<SysUser>> GetListDefaultByTenantAsync(Guid tenantId)
        {
            return await DbSet.Where(w => w.SysTenantId == tenantId && w.IsDefault).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户</returns>
        public async Task<SysUser> GetAsync(string username)
        {
            return await DbSet.Where(w => w.UserName == username).FirstOrDefaultAsync();
        }
    }
}
