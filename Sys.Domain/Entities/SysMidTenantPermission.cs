using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.Entities
{
    /// <summary>
    /// 关联表：租户权限
    /// </summary>
    public partial class SysMidTenantPermission
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public Guid SysPermissionId { get; set; }
    }
}
