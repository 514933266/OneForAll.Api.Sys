using Sys.Domain.Aggregates;
using OneForAll.Core;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信公众号消息回复查询服务（只读）
    /// </summary>
    public interface ISysWechatGzhReplySettingQueryService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWechatGzhReplySettingAggr>> GetPageAsync(int pageIndex, int pageSize, string appId);
    }
}
