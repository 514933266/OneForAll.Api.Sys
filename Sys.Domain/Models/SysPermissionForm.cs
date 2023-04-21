using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 权限表单
    /// </summary>
    public class SysPermissionForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 所属菜单Id
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 代码（由后端开发人员填写，规则为Action的名称）
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 排序代码
        /// </summary>
        [StringLength(20)]
        public string SortCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(100)]
        public string Remark { get; set; }
    }
}
