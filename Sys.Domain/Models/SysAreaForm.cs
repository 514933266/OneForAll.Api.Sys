using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 地区表单
    /// </summary>
    public class SysAreaForm
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        [Required]
        public int ParentId { get; set; }

        /// <summary>
        /// 地区代码（下级地区继承上级，如00,0021,002133）
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [StringLength(4)]
        public string ShortName { get; set; }

    }
}
