using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core.OAuth;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatLoginUsersController : BaseController
    {
        private readonly ISysWechatLoginUserService _userService;
        public SysWechatLoginUsersController(ISysWechatLoginUserService userService)
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
        [Route("{mobile}/MobileUser")]
        public async Task<SysWechatLoginUserDto> GetByMobileAsync(string mobile, [FromQuery] Guid tenantId)
        {
            return await _userService.GetByMobileAsync(tenantId, mobile);
        }

        /// <summary>
        /// 根据UnionId获取微信登录用户
        /// </summary>
        /// <param name="unionId">微信</param>
        /// <param name="tenantId">机构id</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        [Route("{unionId}/UnionIdUser")]
        public async Task<SysWechatLoginUserDto> GetByUnionIdAsync(string unionId, [FromQuery] Guid? tenantId)
        {
            var tid = tenantId ?? Guid.Empty;
            return await _userService.GetByUnionIdAsync(tid, unionId);
        }
    }
}
