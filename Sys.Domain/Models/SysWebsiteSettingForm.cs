using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 网站设置
    /// </summary>
    public class SysWebsiteSettingForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Host { get; set; }

        /// <summary>
        /// 客户端
        /// </summary>
        [Required]
        [StringLength(20)]
        public string OAuthClientId { get; set; }

        /// <summary>
        /// 客户端密码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string OAuthClientSecret { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OAuthClientName { get; set; }

        /// <summary>
        /// 授权地址
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string OAuthUrl { get; set; }
    }
}
