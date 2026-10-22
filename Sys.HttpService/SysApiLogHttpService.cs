using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.OAuth;
using Sys.HttpService.Interfaces;
using Sys.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Sys.HttpService
{
    /// <summary>
    /// Api日志
    /// </summary>
    public class SysApiLogHttpService : BaseHttpService, ISysApiLogHttpService
    {
        private readonly HttpServiceConfig _config;
        private readonly HttpServiceLogConfig _logConfig;

        public SysApiLogHttpService(
            HttpServiceConfig config,
            HttpServiceLogConfig logConfig,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
            _logConfig = logConfig;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns></returns>
        public async Task AddAsync(SysApiLogRequest form)
        {
            // 检查是否启用日志
            if (!_logConfig.ApiLog)
                return;

            try
            {
                if (!LoginUser.Id.IsNullOrEmpty())
                {
                    form.CreatorId = LoginUser.Id;
                    form.CreatorName = LoginUser.DisplayName;
                    form.TenantId = LoginUser.TenantId;
                }

                var client = GetHttpClient(_config.SysLog);
                if (client != null && client.BaseAddress != null)
                {
                    await client.PostAsync("api/SysApiLogs", form, new JsonMediaTypeFormatter());
                }
            }
            catch
            {
                // 忽略异常
            }
        }
    }
}