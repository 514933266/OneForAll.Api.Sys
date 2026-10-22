using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Enums;
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

        /// <summary>
        /// 是否存在指定appId与消息类型的回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <returns>是否存在</returns>
        public async Task<bool> ExistsByAppIdMsgTypeAsync(string appId, SysWechatGzhMsgTypeEnum msgType)
        {
            return await DbSet.AnyAsync(w => w.AppId == appId && w.MsgType == msgType);
        }

        /// <summary>
        /// 按appId与消息类型查询回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <returns>回复实体</returns>
        public async Task<SysWechatGzhReplySetting> GetByAppIdMsgTypeAsync(string appId, SysWechatGzhMsgTypeEnum msgType)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.AppId == appId && w.MsgType == msgType);
        }

        /// <summary>
        /// 按appId、消息类型与关键字查询回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="msgKey">消息关键字</param>
        /// <returns>回复实体</returns>
        public async Task<SysWechatGzhReplySetting> GetByAppIdMsgTypeKeyAsync(string appId, SysWechatGzhMsgTypeEnum msgType, string msgKey)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.AppId == appId && w.MsgType == msgType && w.MsgKey == msgKey);
        }

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatGzhReplySetting>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}

