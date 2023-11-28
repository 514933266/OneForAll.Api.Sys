using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class SysContactUsForm
    {
        /// <summary>
        /// 姓名/公司名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱/电话
        /// </summary>
        [Required]
        public string Contact { get; set; }

        /// <summary>
        /// 留言
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}
