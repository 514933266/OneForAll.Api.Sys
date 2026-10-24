using Microsoft.AspNetCore.Http;
using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using Sys.Domain.Interfaces;
using System.Linq;

namespace Sys.Host.Providers
{
    /// <summary>
    /// 当前登录用户端口实现：从 JWT Claims 解析登录用户（原 OneForAll.Core.BaseManager.LoginUser 逻辑搬家）
    /// </summary>
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public LoginUser LoginUser
        {
            get
            {
                var claims = _httpContextAccessor.HttpContext?.User?.Claims;
                if (claims != null && claims.Any())
                {
                    return new LoginUser
                    {
                        DisplayName = claims.FirstOrDefault(e => e.Type == UserClaimType.DisplayName)?.Value ?? "",
                        UserName = claims.FirstOrDefault(e => e.Type == UserClaimType.UserName)?.Value ?? "",
                        WxAppId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxAppId)?.Value ?? "",
                        WxOpenId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxOpenId)?.Value ?? "",
                        WxUnionId = claims.FirstOrDefault(e => e.Type == UserClaimType.WxUnionId)?.Value ?? "",
                        Id = claims.FirstOrDefault(e => e.Type == UserClaimType.UserId)?.Value ?? "",
                        TenantId = claims.FirstOrDefault(e => e.Type == UserClaimType.TenantId)?.Value ?? "",
                        IsDefault = claims.FirstOrDefault(e => e.Type == UserClaimType.IsDefault)?.Value.TryBoolean() ?? false
                    };
                }
                return new LoginUser();
            }
        }

        /// <summary>
        /// 当前请求的站点基址（如 https://host）
        /// </summary>
        public string BaseUrl
        {
            get
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                if (request == null) return "";
                return request.Scheme + "://" + request.Host.Value;
            }
        }
    }
}
