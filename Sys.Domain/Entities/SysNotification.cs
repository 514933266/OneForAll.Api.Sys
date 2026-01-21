using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Entities
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class SysNotification
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        [Required]
        public bool IsPublish { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public SysNotificationTypeEnum Type { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; } = DateTime.UtcNow;
    }
}

