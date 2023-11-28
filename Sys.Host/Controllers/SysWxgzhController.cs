using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Application.Interfaces;
using Sys.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OneForAll.Core;
using OneForAll.Core.Security;
using System.IO;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    [Route("api/[controller]")]
    public class SysWxgzhController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly ISysWxgzhService _service;
        public SysWxgzhController(IConfiguration config, ISysWxgzhService service)
        {
            _config = config;
            _service = service;
        }

        /// <summary>
        /// 微信接入校验
        /// </summary>
        /// <param name="signature">加密签名</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="nonce">随机数</param>
        /// <param name="echostr">随机字符串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> VerifyIdentityAsync([FromQuery] string signature, [FromQuery] string timestamp, [FromQuery] string nonce, [FromQuery] string echostr)
        {
            var token = _config["Wxgzh:IdentityToken"];
            string[] array = { token, timestamp, nonce };
            Array.Sort(array);
            var str = SHA1Helper.Encrypt(string.Join("", array));
            if (signature.ToLower() == str.ToLower())
                return echostr;
            return "身份异常";
        }

        /// <summary>
        /// 用户事件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> UserEventAsync()
        {
            var appId = _config["Wxgzh:AppId"];
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body))
            {
                var xmlContent = stream.ReadToEndAsync().GetAwaiter().GetResult();
                return await _service.UserEventAsync(appId, xmlContent);
            }
        }
    }
}
