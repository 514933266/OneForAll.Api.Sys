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
    /// 权限
    /// </summary>
    public class SysPermissionRepository : Repository<SysPermission>, ISysPermissionRepository
    {
        public SysPermissionRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 查询分页（含菜单）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysPermissionAggr>> GetPageWithMenuAsync(int pageIndex, int pageSize, string key, Guid menuId)
        {
            var predicate = PredicateBuilder.Create<SysPermission>(w => true);
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.Code.Contains(key) || w.Name.Contains(key));
            if (menuId != Guid.Empty)
                predicate = predicate.And(w => w.SysMenuId == menuId);

            var menuDbSet = Context.Set<SysMenu>();
            var total = await DbSet.CountAsync(predicate);

            var query = (from perm in DbSet.Where(predicate)
                         join menu in menuDbSet on perm.SysMenuId equals menu.Id
                         select new SysPermissionAggr()
                         {
                             Id = perm.Id,
                             Name = perm.Name,
                             Code = perm.Code,
                             Remark = perm.Remark,
                             SortCode = perm.SortCode,
                             SysMenu = menu
                         });

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<SysPermissionAggr>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="menuIds">菜单id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<SysPermission>> GetListByMenuAsync(IEnumerable<Guid> menuIds)
        {
            return await DbSet.Where(w => menuIds.Contains(w.SysMenuId)).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<SysPermission>> GetListByMenuAsync(Guid menuId)
        {
            return await DbSet.Where(w => w.SysMenuId == menuId).OrderBy(o => o.SortCode).ToListAsync();
        }

        /// <summary>
        /// 查询租户权限
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SysPermission>> GetListByTenantAsync(Guid tenantId)
        {
            var tenantDbSet = Context.Set<SysTenant>().Where(w => w.Id == tenantId);
            var contactDbSet = Context.Set<SysTenantPermContact>();

            var data = (from tenant in tenantDbSet
                        join contact in contactDbSet on tenant.Id equals contact.SysTenantId
                        join perm in DbSet on contact.SysPermissionId equals perm.Id
                        select perm);
            return await data.ToListAsync();
        }

        /// <summary>
        /// 查询菜单查看权限
        /// </summary>
        /// <param name="menuIds">菜单id</param>
        /// <returns></returns>
        public async Task<IEnumerable<SysPermission>> GetListEnterViewByMenuAsync(IEnumerable<Guid> menuIds)
        {
            var menuDbSet = Context.Set<SysMenu>().Where(w => menuIds.Contains(w.Id));

            var data = (from menu in menuDbSet
                        join perm in DbSet on menu.Id equals perm.SysMenuId
                        where perm.Name == "EnterView"
                        select perm);
            return await data.ToListAsync();
        }
    }
}
