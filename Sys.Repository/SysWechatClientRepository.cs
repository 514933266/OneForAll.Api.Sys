using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public class SysWechatClientRepository : Repository<SysWechatClient>, ISysWechatClientRepository
    {
        public SysWechatClientRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询微信客户端信息
        /// </summary>
        /// <returns>系统用户</returns>
        public async Task<IEnumerable<SysWechatClientAggr>> GetListWithClientAsync()
        {
            var clientDbSet = Context.Set<SysClient>();
            var contactDbSet = Context.Set<SysMidWechatClient>();
            var sql = (from client in clientDbSet
                       join contact in contactDbSet on client.Id equals contact.SysClientId
                       join wxClient in DbSet on contact.SysWxClientId equals wxClient.Id
                       select new SysWechatClientAggr()
                       {
                           Id = wxClient.Id,
                           AppId = wxClient.AppId,
                           AppSecret = wxClient.AppSecret,
                           AccessToken = wxClient.AccessToken,
                           AccessTokenExpiresIn = wxClient.AccessTokenExpiresIn,
                           AccessTokenCreateTime = wxClient.AccessTokenCreateTime,
                           SysClient = client
                       });

            return await sql.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 查询指定客户端对应的微信客户端信息
        /// </summary>
        /// <param name="clientId">系统客户端</param>
        /// <returns>系统用户</returns>
        public async Task<SysWechatClientAggr> GetByClientIdAsync(string clientId)
        {
            var clientDbSet = Context.Set<SysClient>().Where(w => w.ClientId == clientId);
            var contactDbSet = Context.Set<SysMidWechatClient>();
            var sql = (from client in clientDbSet
                       join contact in contactDbSet on client.Id equals contact.SysClientId
                       join wxClient in DbSet on contact.SysWxClientId equals wxClient.Id
                       select new SysWechatClientAggr()
                       {
                           Id = wxClient.Id,
                           AppId = wxClient.AppId,
                           AppSecret = wxClient.AppSecret,
                           AccessToken = wxClient.AccessToken,
                           AccessTokenExpiresIn = wxClient.AccessTokenExpiresIn,
                           AccessTokenCreateTime = wxClient.AccessTokenCreateTime,
                           SysClient = client
                       });

            return await sql.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询指定appId对应的微信客户端信息
        /// </summary>
        /// <param name="appIds">微信appId</param>
        /// <returns>系统用户</returns>
        public async Task<IEnumerable<SysWechatClientAggr>> GetListByAppIdAsync(List<string> appIds)
        {
            var clientDbSet = Context.Set<SysClient>();
            var contactDbSet = Context.Set<SysMidWechatClient>();
            var sql = (from client in clientDbSet
                       join contact in contactDbSet on client.Id equals contact.SysClientId
                       join wxClient in DbSet on contact.SysWxClientId equals wxClient.Id
                       where (appIds.Contains(wxClient.AppId))
                       select new SysWechatClientAggr()
                       {
                           Id = wxClient.Id,
                           AppId = wxClient.AppId,
                           AppSecret = wxClient.AppSecret,
                           AccessToken = wxClient.AccessToken,
                           AccessTokenExpiresIn = wxClient.AccessTokenExpiresIn,
                           AccessTokenCreateTime = wxClient.AccessTokenCreateTime,
                           SysClient = client
                       });

            return await sql.ToListAsync();
        }

        /// <summary>
        /// 按appId查询微信客户端实体
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <returns>微信客户端</returns>
        public async Task<SysWechatClient> GetByAppIdAsync(string appId)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.AppId == appId);
        }

        /// <summary>
        /// 是否存在指定appId
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsByAppIdAsync(string appId)
        {
            return await DbSet.AnyAsync(w => w.AppId == appId);
        }

        /// <summary>
        /// 是否存在指定appId（排除自身）
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="excludeId">排除的实体id</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsByAppIdAsync(string appId, Guid excludeId)
        {
            return await DbSet.AnyAsync(w => w.AppId == appId && w.Id != excludeId);
        }

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatClient>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}
