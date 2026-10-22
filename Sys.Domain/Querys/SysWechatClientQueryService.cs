using Sys.Domain.Aggregates;
using Sys.Domain.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 微信客户端查询服务（只读，返回聚合根，不含 DTO）
    /// </summary>
    public class SysWechatClientQueryService : ISysWechatClientQueryService
    {
        private readonly ISysWechatClientRepository _repository;

        public SysWechatClientQueryService(ISysWechatClientRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatClientAggr>> GetListAsync()
        {
            return await _repository.GetListWithClientAsync();
        }

        /// <summary>
        /// 按appId集合获取微信客户端
        /// </summary>
        /// <param name="appIds">微信appId</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatClientAggr>> GetListByAppIdAsync(List<string> appIds)
        {
            return await _repository.GetListByAppIdAsync(appIds);
        }

        /// <summary>
        /// 按客户端id获取微信客户端
        /// </summary>
        /// <param name="clientId">系统客户端</param>
        /// <returns>微信客户端</returns>
        public async Task<SysWechatClientAggr> GetByClientIdAsync(string clientId)
        {
            return await _repository.GetByClientIdAsync(clientId);
        }
    }
}
