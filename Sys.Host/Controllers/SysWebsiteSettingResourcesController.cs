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
    /// 网站设置
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWebsiteSettingResourcesController : BaseController
    {
        private readonly ISysWebsiteSettingService _service;
        public SysWebsiteSettingResourcesController(ISysWebsiteSettingService service)
        {
            _service = service;
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <returns>实体</returns>
        [HttpGet]
        [Route("Current")]
        public async Task<SysWebsiteSettingDto> GetAsync()
        {
            var origin = Request.Headers["Origin"].ToString();
            return await _service.GetAsync(origin);
        }
    }
}
