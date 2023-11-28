using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.HttpService.Models
{
    /// <summary>
    /// 微信UnionId
    /// </summary>
    public class WxUnionIdResponse
    {
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        [JsonProperty("subscribe")]
        public string Subscribe { get; set; }

        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        [JsonProperty("openid")]
        public string OpenId { get; set; }

        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        [JsonProperty("subscribe_time")]
        public string SubscribeTime { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台账号后，才会出现该字段。
        /// </summary>
        [JsonProperty("unionid")]
        public string UnionId { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }
    }
}
