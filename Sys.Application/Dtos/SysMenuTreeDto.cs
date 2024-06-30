using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 实体：菜单树
    /// </summary>
    public class SysMenuTreeDto : Entity<Guid>, IChildren<SysMenuTreeDto>, IParent<Guid>
    {

        /// <summary>
        /// 父级id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型 0节点 1远程组件 2页面
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 打开方式 0标签内打开 1新标签打开 2新窗口打开
        /// </summary>
        public int OpenType { get; set; }

        /// <summary>
        /// 菜单代码（由开发人员填写，值为Controller名称）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 页面路由
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Api地址
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否启用（启用的菜单才能被加载出来）
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否默认（默认菜单不可删除）
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 子级
        /// </summary>
        public IEnumerable<SysMenuTreeDto> Children { get; set; } = new List<SysMenuTreeDto>();

        /// <summary>
        /// 权限
        /// </summary>
        public IEnumerable<SysMenuPermissionDto> Permissions { get; set; } = new List<SysMenuPermissionDto>();
    }
}
