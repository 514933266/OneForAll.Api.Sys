using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.Entities
{
    /// <summary>
    /// 关联表：用户权限
    /// </summary>
    public partial class SysMidUserPermission
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public Guid SysPermissionId { get; set; }
    }
}
