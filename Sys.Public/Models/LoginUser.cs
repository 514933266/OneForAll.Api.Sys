﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Public.Models
{
    /// <summary>
    /// 系统登录用户
    /// </summary>
    public class LoginUser
    {
        /// <summary>
        /// 所属租户Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属租户id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
