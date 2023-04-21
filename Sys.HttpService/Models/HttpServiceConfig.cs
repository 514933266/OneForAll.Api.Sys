using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sys.HttpService.Models
{
    /// <summary>
    /// 数据资源服务配置
    /// </summary>
    public class HttpServiceConfig
    {
        /// <summary>
        /// 权限验证接口
        /// </summary>
        public string SysPermissionCheck { get; set; } = "SysPermissionCheck";

        /// <summary>
        /// Api日志
        /// </summary>
        public string SysApiLog { get; set; } = "SysApiLog";

        /// <summary>
        /// 消息通知
        /// </summary>
        public string UmsMessage { get; set; } = "UmsMessage";
    }
}
