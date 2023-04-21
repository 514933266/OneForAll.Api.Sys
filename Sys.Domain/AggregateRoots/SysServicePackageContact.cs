using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 套餐内容
    /// </summary>
    public class SysServicePackageContact : Entity<Guid>
    {
        /// <summary>
        /// 套餐id
        /// </summary>
        [Required]
        public Guid PackageId { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        [Required]
        public Guid ServiceId { get; set; }
    }
}
