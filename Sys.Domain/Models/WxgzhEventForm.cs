using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 微信公众号订阅事件
    /// </summary>
    [Serializable]
    [XmlRoot("xml")]
    public class WxgzhEventForm
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方账号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 消息类型，event
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        public string Event { get; set; }
    }
}
