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
    /// 系统客户端
    /// </summary>
    public class SysClientRepository : Repository<SysClient>, ISysClientRepository
    {
        public SysClientRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 是否存在指定ClientId
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsByClientIdAsync(string clientId)
        {
            return await DbSet.AnyAsync(w => w.ClientId == clientId);
        }

        /// <summary>
        /// 是否存在指定ClientId（排除自身）
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="excludeId">排除的实体id</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsByClientIdAsync(string clientId, Guid excludeId)
        {
            return await DbSet.AnyAsync(w => w.ClientId == clientId && w.Id != excludeId);
        }

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysClient>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}
