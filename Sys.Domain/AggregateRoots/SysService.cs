using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 系统服务
    /// </summary>
    public class SysService : Entity<Guid>
    {
        /// <summary>
        /// 服务代码(唯一)
        /// </summary>
        [Required]
        [Unique]
        [MaxLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        [Required]
        public SysServiceEnum Type { get; set; }

        /// <summary>
        /// 服务内容
        /// </summary>
        [Required]
        [MaxLength(500)]
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
        /// 是否启用
        /// </summary>
        [Required]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
