using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core.OAuth;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatGzhNotifyUsersController : BaseController
    {
        private readonly ISysWechatGzhSubscriberService _service;
        public SysWechatGzhNotifyUsersController(ISysWechatGzhSubscriberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端Id</param>
        /// <returns>用户</returns>
        [HttpGet]
        public async Task<SysWechatGzhSubscriberTokenDto> GetAsync([FromQuery] Guid userId, [FromQuery] string clientId)
        {
            return await _service.GetAsync(userId, clientId);
        }
    }
}
