﻿using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 仓储：角色
    /// </summary>
    public class SysRoleRepository : Repository<SysRole>, ISysRoleRepository
    {
        public SysRoleRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页（包含关联权限、用户）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>结果</returns>
        public async Task<PageList<SysRole>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<SysRole>(w => true);
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Name.Contains(key));

            var total = await DbSet
                .CountAsync(predicate);

            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<SysRole>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="name">名称</param>
        /// <returns>结果</returns>
        public async Task<SysRole> GetByNameAsync(string name)
        {
            return await DbSet
                .Where(w => w.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">角色id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysRole>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <returns>用户</returns>
        public async Task<IEnumerable<SysRole>> GetListByTenantAsync(Guid tenantId)
        {
            return await DbSet.Where(w => w.SysTenantId == tenantId).ToListAsync();
        }
    }
}
