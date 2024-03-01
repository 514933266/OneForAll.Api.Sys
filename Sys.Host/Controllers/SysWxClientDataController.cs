using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using System.Collections.Generic;
using Sys.Public.Models;
using Sys.Host.Filters;
using OneForAll.Core.OAuth;
using Sys.Domain.Repositorys;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信客户端数据
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysWxClientDataController : BaseController
    {
        private readonly ISysWxClientRepository _repository;

        public SysWxClientDataController(ISysWxClientRepository repository)
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