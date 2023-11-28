using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Host.Models;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using Sys.Domain.Enums;
using Sys.Public.Models;
using Autofac.Core;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    [Route("api/[controller]")]
    public class SysTenantSelectionsController : BaseController
    {
        private readonly ISysTenantSelectionService _service;

        public SysTenantSelectionsController(ISysTenantSelectionService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <returns>租户列表</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<SysTenantSelectionDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        [HttpGet]
        public async Task<IEnumerable<SysTenantSelectionDto>> GetListAsync([FromQuery] string key)
        {
            return await _service.GetListAsync(key);
        }
    }
}