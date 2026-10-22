using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public interface ISysWechatClientRepository : IEFCoreRepository<SysWechatClient>
    {
        /// <summary>
        /// 查询微信客户端信息
        /// </summary>
        /// <returns>系统用户</returns>
        Task<IEnumerable<SysWechatClientAggr>> GetListWithClientAsync();

        /// <summary>
        /// 查询指定appId对应的微信客户端信息
        /// </summary>
        /// <param name="appIds">微信appId</param>
        /// <returns>系统用户</returns>
        Task<IEnumerable<SysWechatClientAggr>> GetListByAppIdAsync(List<string> appIds);

        /// <summary>
        /// 查询指定客户端对应的微信客户端信息
        /// </summary>
        /// <param name="clientId">系统客户端</param>
        /// <returns>系统用户</returns>
        Task<SysWechatClientAggr> GetByClientIdAsync(string clientId);

        /// <summary>
        /// 按appId查询微信客户端实体
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <returns>微信客户端</returns>
        Task<SysWechatClient> GetByAppIdAsync(string appId);

        /// <summary>
        /// 是否存在指定appId
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsByAppIdAsync(string appId);

        /// <summary>
        /// 是否存在指定appId（排除自身）
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="excludeId">排除的实体id</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsByAppIdAsync(string appId, Guid excludeId);

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWechatClient>> GetListByIdsAsync(IEnumerable<Guid> ids);
    }
}
