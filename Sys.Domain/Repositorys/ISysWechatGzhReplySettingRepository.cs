using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信公众号回复
    /// </summary>
    public interface ISysWechatGzhReplySettingRepository : IEFCoreRepository<SysWechatGzhReplySetting>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWechatGzhReplySetting>> GetPageAsync(int pageIndex, int pageSize, string appId);

        /// <summary>
        /// 是否存在指定appId与消息类型的回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsByAppIdMsgTypeAsync(string appId, SysWechatGzhMsgTypeEnum msgType);

        /// <summary>
        /// 按appId与消息类型查询回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <returns>回复实体</returns>
        Task<SysWechatGzhReplySetting> GetByAppIdMsgTypeAsync(string appId, SysWechatGzhMsgTypeEnum msgType);

        /// <summary>
        /// 按appId、消息类型与关键字查询回复
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="msgType">消息类型</param>
        /// <param name="msgKey">消息关键字</param>
        /// <returns>回复实体</returns>
        Task<SysWechatGzhReplySetting> GetByAppIdMsgTypeKeyAsync(string appId, SysWechatGzhMsgTypeEnum msgType, string msgKey);

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWechatGzhReplySetting>> GetListByIdsAsync(IEnumerable<Guid> ids);
    }
}
