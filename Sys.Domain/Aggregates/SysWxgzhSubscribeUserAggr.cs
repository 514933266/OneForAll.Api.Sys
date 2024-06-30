using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Aggregates
{
    /// <summary>
    /// 微信公众号关注用户
    /// </summary>
    public class SysWxgzhSubscribeUserAggr : SysWxgzhSubscribeUser
    {
        public SysUser SysUser { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }
    }
}
