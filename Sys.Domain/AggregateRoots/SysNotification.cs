using OneForAll.Core;
using OneForAll.Core.DDD;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public class SysNotification : AggregateRoot<Guid>, ICreateTime, IUpdateTime
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(100)]
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
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
    }
}

