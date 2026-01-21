using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public class SysWebsiteApiSettingForm
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        /// <summary>
        /// 请求的域名
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Host { get; set; }
    }
}
