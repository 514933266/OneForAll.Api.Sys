using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 系统客户端
    /// </summary>
    public class SysClientDto
    {
        /// <summary>
        /// 实体id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 系统客户端id
        /// </summary>]
        public string ClientId { get; set; }

        /// <summary>
        /// 系统客户端密码
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 系统客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 允许自动创建账号
        /// </summary>
        public bool AutoCreateAccount { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public SysClientTypeEnum Type { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
