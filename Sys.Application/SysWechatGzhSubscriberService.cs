using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 微信公众号关注
    /// </summary>
    public class SysWechatGzhSubscriberService : ISysWechatGzhSubscriberService
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatGzhSubscriberQueryService _subQuery;
        private readonly ISysWechatClientQueryService _clientQuery;
        private readonly ISysWechatGzhManager _manager;
        public SysWechatGzhSubscriberService(IMapper mapper, ISysWechatGzhSubscriberQueryService subQuery, ISysWechatClientQueryService clientQuery, ISysWechatGzhManager manager)
        {
            _mapper = mapper;
            _subQuery = subQuery;
            _clientQuery = clientQuery;
            _manager = manager;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户</returns>
        public async Task<PageList<SysWechatGzhSubscriberDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _subQuery.GetPageAsync(pageIndex, pageSize, key);

            var items = _mapper.Map<IEnumerable<SysWechatGzhSubscriberAggr>, IEnumerable<SysWechatGzhSubscriberDto>>(data.Items);
            var appIds = data.Items.Select(s => s.AppId).ToList();
            if (appIds.Any())
            {
                var clients = await _clientQuery.GetListByAppIdAsync(appIds);
                foreach (var item in items)
                {
                    var client = clients.FirstOrDefault(w => w.AppId == item.AppId);
                    if (client != null)
                    {
                        item.ClientName = client.SysClient.ClientName;
                    }
                }
            }

            return new PageList<SysWechatGzhSubscriberDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 获取微信公众号关注用户列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>用户</returns>
        public async Task<IEnumerable<SysWechatGzhSubscriberDto>> GetListAsync([FromBody] IEnumerable<Guid> userIds)
        {
            var data = await _subQuery.GetListByUserAsync(userIds);
            return _mapper.Map<IEnumerable<SysWechatGzhSubscriberAggr>, IEnumerable<SysWechatGzhSubscriberDto>>(data);
        }

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端id</param>
        /// <returns>用户</returns>
        public async Task<SysWechatGzhSubscriberTokenDto> GetAsync(Guid userId, string clientId)
        {
            var result = new SysWechatGzhSubscriberTokenDto() { IsUnSubscribed = true };
            var client = await _clientQuery.GetByClientIdAsync(clientId);
            if (client != null)
            {
                var data = await _subQuery.GetActiveByUserAsync(userId, client.AppId);
                if (data != null)
                {
                    result = _mapper.Map<SysWechatGzhSubscriberTokenDto>(data);
                    result.AppAccessToken = client.AccessToken;
                }
            }
            return result;
        }

        /// <summary>
        /// 关联微信关注用户（补全UnionId并绑定系统用户）
        /// </summary>
        /// <returns>关联结果</returns>
        public async Task<SysWechatGzhSubscriberSynResult> SynSubscribersAsync()
        {
            return await _manager.SynSubscribersAsync();
        }
    }
}
