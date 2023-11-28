using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class SysContactUs
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

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

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
