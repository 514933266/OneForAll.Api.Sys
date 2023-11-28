using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Aggregates
{
    /// <summary>
    /// 微信公众号回复
    /// </summary>
    public class SysWxgzhReplySettingAggr : SysWxgzhReplySetting
    {
        /// <summary>
        /// 所属公众号名称
        /// </summary>
        public string ClientName { get; set; }
    }
}
