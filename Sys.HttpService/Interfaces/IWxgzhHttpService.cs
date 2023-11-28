using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Sys.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sys.HttpService.Interfaces
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public interface IWxgzhHttpService
    {
        /// <summary>
        /// 获取UnionId
        /// </summary>
        /// <param name="openid">微信openid</param>
        /// <param name="accessToken">公众号密钥</param>
        /// <returns>登录结果</returns>
        Task<WxUnionIdResponse> GetUnionIdAsync(string openid, string accessToken);

        /// <summary>
        /// 发送客服消息
        /// </summary>
        /// <param name="msg">发送的消息</param>
        /// <param name="accessToken">公众号密钥</param>
        /// <returns>登录结果</returns>
        Task SendCustomerMessageAsync(string msg, string accessToken);
    }
}
