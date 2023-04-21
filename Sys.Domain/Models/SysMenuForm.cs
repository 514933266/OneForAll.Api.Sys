using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sys.Domain.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysMenuForm : Entity<Guid>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        [Required]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 类型 0节点 1远程组件 2页面
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 打开方式 0标签内打开 1新标签打开 2新窗口打开
        /// </summary>
        [Required]
        public int OpenType { get; set; }

        /// <summary>
        /// 菜单代码（由后端开发人员填写，值为Controller名称，如无填写None）
        /// </summary>
        [StringLength(200)]
        public string Code { get; set; }

        /// <summary>
        /// 页面路径（由后端开发人员填写，具体根据前端配置而定）
        /// </summary>
        [StringLength(300)]
        public string Url { get; set; }

        /// <summary>
        /// Api地址
        /// </summary>
        [StringLength(300)]
        public string ApiUrl { get; set; }

        /// <summary>
        /// 菜单图标（使用fontawesome图标）
        /// </summary>
        [StringLength(300)]
        public string Icon { get; set; }

        /// <summary>
        /// 是否启用（启用的菜单才能被加载出来）
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否默认（默认菜单不可删除）
        /// </summary>
        [Required]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 排序（由小到大）
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(300)]
        public string Remark { get; set; }

    }
}
