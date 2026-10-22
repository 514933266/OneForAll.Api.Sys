using Microsoft.AspNetCore.Http;
using OneForAll.Core;
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
    /// 异常日志
    /// </summary>
    public class SysExceptionLogHttpService : BaseHttpService, ISysExceptionLogHttpService
    {
        private readonly HttpServiceConfig _config;
        private readonly HttpServiceLogConfig _logConfig;

        public SysExceptionLogHttpService(
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
        public async Task AddAsync(SysExceptionLogRequest form)
        {
            // 检查是否启用日志
            if (!_logConfig.ExceptionLog)
                return;

            try
            {
                form.CreatorId = LoginUser.Id;
                form.CreatorName = LoginUser.DisplayName;
                form.TenantId = LoginUser.TenantId;
                form.CreateTime = DateTime.Now;

                var client = GetHttpClient(_config.SysLog);
                if (client != null && client.BaseAddress != null)
                {
                    await client.PostAsync("api/SysExceptionLogs", form, new JsonMediaTypeFormatter());
                }
            }
            catch
            {
                // 忽略异常
            }
        }
    }
}