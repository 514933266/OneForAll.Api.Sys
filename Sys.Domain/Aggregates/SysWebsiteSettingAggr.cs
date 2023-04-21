using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Aggregates
{
    /// <summary>
    /// 站点配置
    /// </summary>
    public class SysWebsiteSettingAggr : SysWebsiteSetting
    {
        /// <summary>
        /// Api明细
        /// </summary>
        public IEnumerable<SysWebsiteApiSetting> SysWebsiteApiSettings { get; set; }
    }
}
