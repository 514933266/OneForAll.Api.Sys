using OneForAll.EFCore;
using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信客户端-系统客户端关联
    /// </summary>
    public interface ISysMidWechatClientRepository : IEFCoreRepository<SysMidWechatClient>
    {
        /// <summary>
        /// 按系统客户端id与微信客户端id查询关联
        /// </summary>
        /// <param name="clientId">系统客户端id</param>
        /// <param name="wxClientId">微信客户端id</param>
        /// <returns>关联实体</returns>
        Task<SysMidWechatClient> GetByClientAndWxAsync(Guid clientId, Guid wxClientId);

        /// <summary>
        /// 按微信客户端id集合查询关联列表
        /// </summary>
        /// <param name="wxClientIds">微信客户端id</param>
        /// <returns>关联列表</returns>
        Task<IEnumerable<SysMidWechatClient>> GetListByWxClientIdsAsync(IEnumerable<Guid> wxClientIds);
    }
}
