using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 文本事件
    /// </summary>
    [Serializable]
    [XmlRoot("xml")]
    public class WxgzhTextEventForm : WxgzhEventForm
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }
    }
}
