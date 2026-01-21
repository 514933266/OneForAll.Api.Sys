using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.Entities
{
    /// <summary>
    /// 关联表：角色权限
    /// </summary>
    public partial class SysMidRolePermission
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public Guid SysRoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public Guid SysPermissionId { get; set; }
    }
}
