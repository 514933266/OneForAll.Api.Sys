using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using OneForAll.Core.OAuth;
using Sys.Domain.Repositorys;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信客户端数据
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatClientDataController : BaseController
    {
        private readonly ISysWechatClientRepository _repository;

        public SysWechatClientDataController(ISysWechatClientRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("{clientId}/AccessToken")]
        public async Task<string> GetAsync(string clientId)
        {
            var client = await _repository.GetByClientIdAsync(clientId);
            return client == null ? "" : client.AccessToken;
        }
    }
}