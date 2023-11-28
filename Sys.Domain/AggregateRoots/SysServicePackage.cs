using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 服务套餐
    /// </summary>
    public class SysServicePackage : Entity<Guid>
    {
        /// <summary>
        /// 代码(唯一)
        /// </summary>
        [Required]
        [Unique]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,3)")]
        public decimal Price { get; set; }

        /// <summary>
        /// 价格模式
        /// </summary>
        [Required]
        public SysPriceModeEnum PriceMode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 服务Json
        /// </summary>
        [Required]
        public string ServiceJson { get; set; }
    }
}
