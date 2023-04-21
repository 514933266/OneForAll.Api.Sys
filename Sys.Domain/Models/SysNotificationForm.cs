using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public class SysNotificationForm
    {
        public SysNotificationForm()
        {
            ToAccounts = new HashSet<SysNotificationAccountForm>();
        }

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
        /// 类型
        /// </summary>
        [Required]
        public SysNotificationTypeEnum Type { get; set; }

        /// <summary>
        /// 接收者
        /// </summary>
        public IEnumerable<SysNotificationAccountForm> ToAccounts { get; set; }

    }

    /// <summary>
    /// 系统通知-接收用户
    /// </summary>
    public class SysNotificationAccountForm
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
    }
}
