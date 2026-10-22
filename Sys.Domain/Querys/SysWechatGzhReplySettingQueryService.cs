using AutoMapper;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 微信公众号消息回复查询服务（只读，返回聚合根，不含 DTO）
    /// </summary>
    public class SysWechatGzhReplySettingQueryService : ISysWechatGzhReplySettingQueryService
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatGzhReplySettingRepository _repository;
        private readonly ISysWechatClientRepository _clientRepository;

        public SysWechatGzhReplySettingQueryService(
            IMapper mapper,
            ISysWechatGzhReplySettingRepository repository,
            ISysWechatClientRepository clientRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWechatGzhReplySettingAggr>> GetPageAsync(int pageIndex, int pageSize, string appId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            var data = await _repository.GetPageAsync(pageIndex, pageSize, appId);
            var items = _mapper.Map<IEnumerable<SysWechatGzhReplySetting>, IEnumerable<SysWechatGzhReplySettingAggr>>(data.Items);
            var appIds = data.Items.Select(s => s.AppId).ToList();
            if (appIds.Any())
            {
                var clients = await _clientRepository.GetListByAppIdAsync(appIds);
                foreach (var item in items)
                {
                    var client = clients.FirstOrDefault(w => w.AppId == item.AppId);
                    if (client != null)
                    {
                        item.ClientName = client.SysClient.ClientName;
                    }
                }
            }
            return new PageList<SysWechatGzhReplySettingAggr>(data.Total, data.PageSize, data.PageIndex, items);
        }
    }
}
