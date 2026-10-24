using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core.OAuth;
using Sys.Application.Interfaces;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信客户端数据
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatClientDataController : BaseController
    {
        private readonly ISysWechatClientService _clientService;

        public SysWechatClientDataController(ISysWechatClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        [Route("{clientId}/AccessToken")]
        public async Task<string> GetAsync(string clientId)
        {
            return await _clientService.GetAccessTokenAsync(clientId);
        }
    }
}
