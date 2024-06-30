using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Enums
{
    /// <summary>
    /// 微信公众号关注方式
    /// </summary>
    public enum SysWxgzhSubscribeType
    {
        /// <summary>
        /// 公众号搜索
        /// </summary>
        Search = 1000,

        /// <summary>
        /// 公众号迁移
        /// </summary>
        Migration = 1001,

        /// <summary>
        /// 二维码
        /// </summary>
        QRCode = 1002,

        /// <summary>
        /// 公众号名片
        /// </summary>
        ProfileCard = 1003,

        /// <summary>
        /// 图文页内名称点击
        /// </summary>
        ProfileLink = 1004,

        /// <summary>
        /// 图文页右上角菜单
        /// </summary>
        ProfileItem = 1005,

        /// <summary>
        /// 支付后关注
        /// </summary>
        Paid = 1006,

        /// <summary>
        /// 微信广告
        /// </summary>
        WechatAdvertisement = 1007,

        /// <summary>
        /// 他人转载 
        /// </summary>
        RepRint = 1008,

        /// <summary>
        /// 视频号直播 
        /// </summary>
        LiveStream = 1009,

        /// <summary>
        /// 视频号 
        /// </summary>
        Channels = 1010,

        /// <summary>
        /// 其他 
        /// </summary>
        Other = 99,
    }
}
