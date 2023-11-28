using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.ValueObjects
{
    /// <summary>
    /// 微信公众号消息返回内容设置
    /// </summary>
    public class SysWxgzhReplySettingContentVo
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 替换值
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}
