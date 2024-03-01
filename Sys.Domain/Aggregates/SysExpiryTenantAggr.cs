using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Aggregates
{
    /// <summary>
    /// 已到期租户
    /// </summary>
    public class SysExpiryTenantAggr : SysTenant
    {
        /// <summary>
        /// 服务到期时间（到期后需要续费）
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
    }
}
