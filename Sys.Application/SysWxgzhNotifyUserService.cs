using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
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
    public class SysWxgzhNotifyUserService : ISysWxgzhNotifyUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysWxgzhSubscribeUserRepository _repository;
        private readonly ISysWxClientRepository _clientRepository;
        public SysWxgzhNotifyUserService(IMapper mapper, ISysWxgzhSubscribeUserRepository repository, ISysWxClientRepository clientRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端id</param>
        /// <returns>用户</returns>
        public async Task<SysWxgzhNotifyUserDto> GetAsync(Guid userId, string clientId)
        {
            var result = new SysWxgzhNotifyUserDto();
            var client = await _clientRepository.GetByClientIdAsync(clientId);
            if (client != null)
            {
                var data = await _repository.GetAsync(w => w.SysUserId == userId && w.AppId == client.AppId && !w.IsUnSubscribed);
                if (data != null)
                {
                    result = _mapper.Map<SysWxgzhSubscribeUser, SysWxgzhNotifyUserDto>(data);
                    result.AppAccessToken = client.AccessToken;
                }
            }
            return result;
        }
    }
}
