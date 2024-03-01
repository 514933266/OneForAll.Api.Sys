using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
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
    /// 租户服务设置
    /// </summary>
    public class SysTenantServiceSetting
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 服务到期时间（到期后需要续费）
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 可用短信数量
        /// </summary>
        [Required]
        public int MobileMsgCount { get; set; }

    }
}
