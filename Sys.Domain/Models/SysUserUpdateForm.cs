using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 修改用户信息
    /// </summary>
    public class SysUserUpdateForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

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
