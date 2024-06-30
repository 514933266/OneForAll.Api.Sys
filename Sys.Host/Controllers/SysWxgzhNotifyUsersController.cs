using System;
using System.Threading.Tasks;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Models;
using Sys.Host.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using System.Collections.Generic;
using Sys.Public.Models;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    [Route("api/[controller]")]
    public class SysWxgzhNotifyUsersController : BaseController
    {
        private readonly ISysWxgzhSubscribeUserService _service;
        public SysWxgzhNotifyUsersController(ISysWxgzhSubscribeUserService service)
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
        public async Task<SysWxgzhSubscribeUserTokenDto> GetAsync([FromQuery] Guid userId, [FromQuery] string clientId)
        {
            return await _service.GetAsync(userId, clientId);
        }
    }
}
