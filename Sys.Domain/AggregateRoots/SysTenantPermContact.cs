﻿using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 关联表：租户权限
    /// </summary>
    public partial class SysTenantPermContact : AggregateRoot<Guid>
    {
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
