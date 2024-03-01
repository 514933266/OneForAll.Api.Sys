using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using OneForAll.Core.DDD;
using Sys.Public.Models;
using Sys.Domain.AggregateRoots;
using OneForAll.Core.OAuth;

namespace Sys.Domain
{
    /// <summary>
    /// 基类
    /// </summary>
    public class SysBaseManager : BaseManager
    {
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public SysBaseManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected LoginUser LoginUser
        {
            get
            {
                var role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == UserClaimType.ROLE);
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == UserClaimType.USER_ID);
                var name = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == UserClaimType.USER_NICKNAME);
                var tenantId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(e => e.Type == UserClaimType.TENANT_ID);

                return new LoginUser()
                {
                    Id = userId == null ? Guid.Empty : new Guid(userId.Value),
                    Name = name == null ? "无" : name?.Value,
                    SysTenantId = tenantId == null ? Guid.Empty : new Guid(tenantId?.Value),
                    IsDefault = role == null ? false : role.Value.Equals(UserRoleType.RULER)
                };
            }
        }
    }
}
