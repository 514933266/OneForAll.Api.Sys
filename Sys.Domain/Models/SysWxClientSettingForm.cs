using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public class SysWxClientSettingForm
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
        /// 微信客户端id
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AppId { get; set; }

        /// <summary>
        /// 微信客户端密码
        /// </summary>
        [Required]
        [StringLength(100)]
        public string AppSecret { get; set; }
    }
}
