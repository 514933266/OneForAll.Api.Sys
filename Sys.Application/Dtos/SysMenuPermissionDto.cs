﻿using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 实体：菜单权限
    /// </summary>
    public class SysMenuPermissionDto : Entity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码（由后端开发人员填写，规则为Action的名称）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序代码
        /// </summary>
        public string SortCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
