using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using OneForAll.Core.Extension;
using Org.BouncyCastle.Asn1.Ocsp;
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
    /// 微信公众号
    /// </summary>
    public class WxgzhHttpService : BaseHttpService, IWxgzhHttpService
    {
        private readonly HttpServiceConfig _config;

        public WxgzhHttpService(
            HttpServiceConfig config,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory) : base(httpContext, httpClientFactory)
        {
            _config = config;
        }

        /// <summary>
        /// 获取UnionId
        /// </summary>
        /// <param name="openid">微信openid</param>
        /// <param name="accessToken">公众号密钥</param>
        /// <returns>登录结果</returns>
        public async Task<WxUnionIdResponse> GetUnionIdAsync(string openid, string accessToken)
        {
            var client = GetHttpClient(_config.WxgzhUnionId);
            if (client != null && client.BaseAddress != null)
            {
                var url = $"{client.BaseAddress}?access_token={accessToken}&openid={openid}&lang=zh_CN";
                var result = await client.GetAsync(url);
                var str = await result.Content.ReadAsStringAsync();
                return str.FromJson<WxUnionIdResponse>();
            }
            return null;
        }

        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="msg">发送的消息</param>
        /// <param name="accessToken">公众号密钥</param>
        /// <returns>登录结果</returns>
        public async Task SendCustomerMessageAsync(string msg, string accessToken)
        {
            var client = GetHttpClient(_config.WxgzhCustomerMsg);
            if (client != null && client.BaseAddress != null)
            {
                var url = $"{client.BaseAddress}?access_token={accessToken}";
                var requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(url),
                    Content = new ByteArrayContent(Encoding.UTF8.GetBytes(msg))
                };
                var result = await client.SendAsync(requestMessage);
                var str = await result.Content.ReadAsStringAsync();
            }
        }
    }
}
