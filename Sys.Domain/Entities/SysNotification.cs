using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OneForAll.Core;
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

        /// <summary>
        /// 发布（聚合行为）：未发布则置为已发布并返回成功；已发布则拒绝井不改变状态。
        /// </summary>
        /// <returns>可发布返回 <see cref="BaseErrType.Success"/>，已发布返回 <see cref="BaseErrType.NotAllow"/></returns>
        public BaseErrType Publish()
        {
            if (IsPublish) return BaseErrType.NotAllow;
            IsPublish = true;
            return BaseErrType.Success;
        }

        /// <summary>
        /// 重置为草稿（聚合行为）：内容变更后撤回发布态，需重新发布。
        /// </summary>
        public void ResetToDraft()
        {
            IsPublish = false;
        }
    }
}

