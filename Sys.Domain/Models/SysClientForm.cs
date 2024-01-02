using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 系统客户端
    /// </summary>
    public class SysClientForm
    {
        /// <summary>
        /// 实体id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 系统客户端id
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ClientId { get; set; }

        /// <summary>
        /// 系统客户端密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// 系统客户端名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string ClientName { get; set; }

        /// <summary>
        /// 允许自动创建账号
        /// </summary>
        [Required]
        public bool AutoCreateAccount { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public SysClientTypeEnum Type { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [StringLength(20)]
        public string Role { get; set; }
    }
}
