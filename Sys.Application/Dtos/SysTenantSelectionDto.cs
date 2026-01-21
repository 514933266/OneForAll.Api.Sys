using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 机构下拉选项
    /// </summary>
    public class SysTenantSelectionDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>

        public Guid ParentId { get; set; }

        /// <summary>
        /// 租户代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 企业Logo
        /// </summary>
        public string LogoUrl { get; set; }
    }
}
