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
    /// 网站设置
    /// </summary>
    public class SysWebsiteSettingRepository : Repository<SysWebsiteSetting>, ISysWebsiteSettingRepository
    {
        public SysWebsiteSettingRepository(DbContext context)
            : base(context)
        {

        }

        #region 列表

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        public async Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<SysWebsiteSetting>(w => true);
            if (!key.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.StartsWith(key) || w.Host.StartsWith(key));
            }

            var total = await DbSet
                .CountAsync(predicate);

            var data = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageList<SysWebsiteSetting>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysWebsiteSetting>> GetListAsTrackingAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
        }

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSetting> GetByHostAsync(string host)
        {
            return await DbSet.Where(w => w.Host == host).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSettingAggr> GetByHostWithContactAsync(string host)
        {
            var apiDbSet = Context.Set<SysWebsiteApiSetting>();

            var query = (from web in DbSet
                         join api in apiDbSet on web.Id equals api.SysWebsiteSettingId
                         where web.Host == host
                         group new
                         {
                             web.Id,
                             web.Host,
                             web.Name,
                             web.OAuthClientId,
                             web.OAuthClientSecret,
                             web.OAuthClientName,
                             web.OAuthUrl,
                             web.LoginBackgroundUrl,
                             Api = api
                         } by web.Id into gData
                         select new SysWebsiteSettingAggr()
                         {
                             Id = gData.FirstOrDefault().Id,
                             Host = gData.FirstOrDefault().Host,
                             Name = gData.FirstOrDefault().Name,
                             OAuthClientId = gData.FirstOrDefault().OAuthClientId,
                             OAuthClientSecret = gData.FirstOrDefault().OAuthClientSecret,
                             OAuthClientName = gData.FirstOrDefault().OAuthClientName,
                             OAuthUrl = gData.FirstOrDefault().OAuthUrl,
                             LoginBackgroundUrl = gData.FirstOrDefault().LoginBackgroundUrl,
                             SysWebsiteApiSettings = gData.Select(s => s.Api).ToList()
                         });
            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}

