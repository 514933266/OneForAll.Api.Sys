using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 系统通知记录（仅记录指定账号）
    /// </summary>
    public class SysNotificationToAccount
    {
        /// <summary>
        /// 消息id
        /// </summary>
        [Required]
        public Guid MessageId { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
    }
}
