﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 系统地区
    /// </summary>
    public class SysAreaDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 地区代码（下级地区继承上级，如00,0021,002133）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 1省 2市 3县区 4镇街
        /// </summary>
        public byte Level { get; set; }
    }
}
