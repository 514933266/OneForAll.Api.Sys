using Sys.Domain.Aggregates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信客户端查询服务（只读）
    /// </summary>
    public interface ISysWechatClientQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWechatClientAggr>> GetListAsync();

        /// <summary>
        /// 按appId集合获取微信客户端
        /// </summary>
        /// <param name="appIds">微信appId</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWechatClientAggr>> GetListByAppIdAsync(List<string> appIds);

        /// <summary>
        /// 按客户端id获取微信客户端
        /// </summary>
        /// <param name="clientId">系统客户端</param>
        /// <returns>微信客户端</returns>
        Task<SysWechatClientAggr> GetByClientIdAsync(string clientId);
    }
}
