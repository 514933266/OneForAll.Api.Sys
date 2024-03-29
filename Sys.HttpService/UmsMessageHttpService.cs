﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OneForAll.Core;
using OneForAll.Core.Extension;
using Sys.HttpService.Interfaces;
using Sys.HttpService.Models;
using Sys.Public.Models;
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
    /// 消息通知
    /// </summary>
    public class UmsMessageHttpService : BaseHttpService, IUmsMessageHttpService
    {
        private readonly HttpServiceConfig _config;

        public UmsMessageHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        public async Task<BaseMessage> SendAsync(UmsMessageRequest form)
        {
            var client = GetHttpClient(_config.UmsMessage);
            if (client != null)
            {
                var msg = await client.PostAsync(client.BaseAddress, form, new JsonMediaTypeFormatter());
                var content = await msg.Content.ReadAsStringAsync();
                return content.FromJson<BaseMessage>();
            }

            throw new Exception("客户端配置异常");
        }
    }
}
