using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.Entities
{
    /// <summary>
    /// 关联表：角色用户
    /// </summary>
    public class SysMidRoleUser
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
        /// 用户Id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }
    }
}
