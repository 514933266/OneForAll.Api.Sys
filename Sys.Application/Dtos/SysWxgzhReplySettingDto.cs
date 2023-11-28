using Sys.Domain.Enums;
using Sys.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 微信公众号消息回复
    /// </summary>
    public class SysWxgzhReplySettingDto
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 应用id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 所属公众号名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public SysWxgzhMsgTypeEnum MsgType { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public SysWxgzhReplyTypeEnum ReplyType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<SysWxgzhReplySettingContentVo> Contents { get; set; } = new List<SysWxgzhReplySettingContentVo>();

    }
}
