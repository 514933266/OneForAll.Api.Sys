using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
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
    public class SysWxgzhSubscribeUserService : ISysWxgzhSubscribeUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysWxgzhSubscribeUserRepository _repository;
        private readonly ISysWxClientRepository _clientRepository;
        public SysWxgzhSubscribeUserService(IMapper mapper, ISysWxgzhSubscribeUserRepository repository, ISysWxClientRepository clientRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户</returns>
        public async Task<PageList<SysWxgzhSubscribeUserDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _repository.GetPageAsync(pageIndex, pageSize, key);

            var items = _mapper.Map<IEnumerable<SysWxgzhSubscribeUserAggr>, IEnumerable<SysWxgzhSubscribeUserDto>>(data.Items);
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

            return new PageList<SysWxgzhSubscribeUserDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 获取微信公众号关注用户列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>用户</returns>
        public async Task<IEnumerable<SysWxgzhSubscribeUserDto>> GetListAsync([FromBody] IEnumerable<Guid> userIds)
        {
            var data = await _repository.GetListByUserAsync(userIds);
            return _mapper.Map<IEnumerable<SysWxgzhSubscribeUserAggr>, IEnumerable<SysWxgzhSubscribeUserDto>>(data);
        }

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端id</param>
        /// <returns>用户</returns>
        public async Task<SysWxgzhSubscribeUserTokenDto> GetAsync(Guid userId, string clientId)
        {
            var result = new SysWxgzhSubscribeUserTokenDto() { IsUnSubscribed = true };
            var client = await _clientRepository.GetByClientIdAsync(clientId);
            if (client != null)
            {
                var data = await _repository.GetAsync(w => w.SysUserId == userId && w.AppId == client.AppId && !w.IsUnSubscribed);
                if (data != null)
                {
                    result = _mapper.Map<SysWxgzhSubscribeUserTokenDto>(data);
                    result.AppAccessToken = client.AccessToken;
                }
            }
            return result;
        }
    }
}
