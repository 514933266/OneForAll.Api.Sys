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
    /// 微信客户端关联
    /// </summary>
    public class SysMidWechatClientRepository : Repository<SysMidWechatClient>, ISysMidWechatClientRepository
    {
        public SysMidWechatClientRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 按系统客户端id与微信客户端id查询关联
        /// </summary>
        /// <param name="clientId">系统客户端id</param>
        /// <param name="wxClientId">微信客户端id</param>
        /// <returns>关联实体</returns>
        public async Task<SysMidWechatClient> GetByClientAndWxAsync(Guid clientId, Guid wxClientId)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.SysClientId == clientId && w.SysWxClientId == wxClientId);
        }

        /// <summary>
        /// 按微信客户端id集合查询关联列表
        /// </summary>
        /// <param name="wxClientIds">微信客户端id</param>
        /// <returns>关联列表</returns>
        public async Task<IEnumerable<SysMidWechatClient>> GetListByWxClientIdsAsync(IEnumerable<Guid> wxClientIds)
        {
            return await DbSet.Where(w => wxClientIds.Contains(w.SysWxClientId)).ToListAsync();
        }
    }
}
