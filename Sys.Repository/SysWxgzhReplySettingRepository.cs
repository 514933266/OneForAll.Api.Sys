using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Extension;
using Sys.Domain.Repositorys;

namespace Sys.Repository
{
    /// <summary>
    /// 微信公众号回复
    /// </summary>
    public class SysWxgzhReplySettingRepository : Repository<SysWxgzhReplySetting>, ISysWxgzhReplySettingRepository
    {
        public SysWxgzhReplySettingRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWxgzhReplySettingAggr>> GetPageAsync(int pageIndex, int pageSize, string appId)
        {
            var predicate = PredicateBuilder.Create<SysWxgzhReplySetting>(w => true);
            if (!appId.IsNullOrEmpty())
                predicate = predicate.And(w => w.AppId == appId);

            var clientDbSet = Context.Set<SysWxClientSetting>();
            var total = await DbSet.CountAsync(predicate);

            var query = (from setting in DbSet.Where(predicate)
                         join client in clientDbSet on setting.AppId equals client.AppId into leftJoinClient
                         from lfClient in leftJoinClient.DefaultIfEmpty()
                         select new SysWxgzhReplySettingAggr()
                         {
                             Id = setting.Id,
                             AppId = setting.AppId,
                             MsgType = setting.MsgType,
                             ReplyType = setting.ReplyType,
                             XmlContent = setting.XmlContent,
                             ContentJson = setting.ContentJson,
                             ClientName = lfClient.ClientName
                         });

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<SysWxgzhReplySettingAggr>(total, pageSize, pageIndex, data);
        }
    }
}

