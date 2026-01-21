using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using OneForAll.Core.OAuth;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatGzhSubscribersController : BaseController
    {
        private readonly ISysWechatGzhSubscriberService _service;
        public SysWechatGzhSubscribersController(ISysWechatGzhSubscriberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        public async Task<PageList<SysWechatGzhSubscriberDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取微信公众号关注用户列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户</returns>
        [HttpGet]
        public async Task<IEnumerable<SysWechatGzhSubscriberDto>> GetListAsync([FromQuery] IEnumerable<Guid> ids)
        {
            return await _service.GetListAsync(ids);
        }
    }
}
