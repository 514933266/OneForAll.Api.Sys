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
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysWxLoginUsersController : BaseController
    {
        private readonly ISysWxLoginUserService _userService;
        public SysWxLoginUsersController(ISysWxLoginUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// 根据电话获取微信登录用户
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="tenantId">机构id</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        [Route("{mobile}/Users")]
        public async Task<SysWxLoginUserDto> GetByMobileAsync(string mobile, [FromQuery] Guid tenantId)
        {
            return await _userService.GetByMobileAsync(tenantId, mobile);
        }
    }
}
