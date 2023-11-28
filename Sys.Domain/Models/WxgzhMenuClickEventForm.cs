using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 微信公众号-菜单点击事件
    /// </summary>
    [Serializable]
    [XmlRoot("xml")]
    public class WxgzhMenuClickEventForm : WxgzhEventForm
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
