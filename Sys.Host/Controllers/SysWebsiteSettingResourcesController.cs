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
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Upload;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 网站设置
    /// </summary>
    [Route("api/[controller]")]
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
