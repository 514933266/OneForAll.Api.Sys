using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
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
    public interface ISysWxClientRepository : IEFCoreRepository<SysWxClient>
    {
        /// <summary>
        /// 查询微信客户端信息
        /// </summary>
        /// <returns>系统用户</returns>
        Task<IEnumerable<SysWxClientAggr>> GetListWithClientAsync();

        /// <summary>
        /// 查询指定客户端对应的微信客户端信息
        /// </summary>
        /// <param name="clientId">系统客户端</param>
        /// <returns>系统用户</returns>
        Task<SysWxClientAggr> GetByClientIdAsync(string clientId);
    }
}
