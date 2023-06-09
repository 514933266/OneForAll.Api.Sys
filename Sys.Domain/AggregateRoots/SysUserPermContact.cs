﻿using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 关联表：用户权限
    /// </summary>
    public partial class SysUserPermContact : AggregateRoot<Guid>
    {
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
