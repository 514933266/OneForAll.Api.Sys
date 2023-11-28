using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class SysContactUsDto
    {
        [Key]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 姓名/公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮箱/电话
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 留言
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
