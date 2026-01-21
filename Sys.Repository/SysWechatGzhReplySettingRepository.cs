using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.Entities;
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
    public class SysWechatGzhReplySettingRepository : Repository<SysWechatGzhReplySetting>, ISysWechatGzhReplySettingRepository
    {
        public SysWechatGzhReplySettingRepository(DbContext context)
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
        public async Task<PageList<SysWechatGzhReplySetting>> GetPageAsync(int pageIndex, int pageSize, string appId)
        {
            var predicate = PredicateBuilder.Create<SysWechatGzhReplySetting>(w => true);
            if (!appId.IsNullOrEmpty())
                predicate = predicate.And(w => w.AppId == appId);
            var total = await DbSet.CountAsync(predicate);

            var query = (from setting in DbSet.Where(predicate)
                         select new SysWechatGzhReplySetting()
                         {
                             Id = setting.Id,
                             AppId = setting.AppId,
                             MsgType = setting.MsgType,
                             ReplyType = setting.ReplyType,
                             XmlContent = setting.XmlContent,
                             ContentJson = setting.ContentJson
                         });

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<SysWechatGzhReplySetting>(total, pageSize, pageIndex, data);
        }
    }
}

