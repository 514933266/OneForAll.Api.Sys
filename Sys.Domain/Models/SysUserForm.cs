﻿using OneForAll.Core.DDD;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUserForm
    {
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

        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(32)]
        public string Password { get; set; }

        /// <summary>
        /// 重复密码
        /// </summary>
        [StringLength(32)]
        public string RePassword { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(20)]
        [RegularExpression("^1[0-9]{10}$", ErrorMessage = "手机号码格式错误")]
        public string Mobile { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public SysUserStatusEnum Status { get; set; }

        /// <summary>
        /// 是否默认（默认用户禁止删除）
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
