using OneForAll.Core.DDD;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 实体：用户
    /// </summary>
    public class SysUserDto : Entity<Guid>
    {
        /// <summary>
        /// 所属租户id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 所属租户名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public SysUserStatusEnum Status { get; set; }

        /// <summary>
        /// 是否默认（默认用户禁止删除）
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 最后登录Ip
        /// </summary>
        public string LastLoginIp { get; set; }
    }
}
