using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 权限验证模型
    /// </summary>
    public class SysPermissionCheck
    {
        /// <summary>
        /// 客户端Id
        /// </summary>
        [Required]
        public string ClientId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 所属租户id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// Controller名称
        /// </summary>
        [Required]
        public string Controller { get; set; }

        /// <summary>
        /// Action的名称
        /// </summary>
        [Required]
        public string Action { get; set; }
    }
}
