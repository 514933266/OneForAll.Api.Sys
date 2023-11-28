using Sys.Domain.Enums;
using Sys.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 微信公众号自定义回复
    /// </summary>
    public class SysWxgzhReplySettingForm
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        [Required]
        [StringLength(200)]
        public string AppId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public SysWxgzhMsgTypeEnum MsgType { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public SysWxgzhReplyTypeEnum ReplyType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public List<SysWxgzhReplySettingContentVo> Contents { get; set; } = new List<SysWxgzhReplySettingContentVo>();
    }
}
