using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Enums
{
    /// <summary>
    /// 公众号回复类型
    /// </summary>
    public enum SysWxgzhReplyTypeEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,

        /// <summary>
        /// 视频类型
        /// </summary>
        Video = 1,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 2,

        /// <summary>
        /// 语音消息
        /// </summary>
        Voice = 3,

        /// <summary>
        /// 音乐
        /// </summary>
        Music = 4,

        /// <summary>
        /// 图文
        /// </summary>
        Article = 5,

        /// <summary>
        /// 客服消息
        /// </summary>
        CustomerService = 6
    }
}
