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
        /// 异常日志
        /// </summary>
        public string SysExceptionLog { get; set; } = "SysExceptionLog";

        /// <summary>
        /// 全局异常日志
        /// </summary>
        public string SysGlobalExceptionLog { get; set; } = "SysGlobalExceptionLog";

        /// <summary>
        /// 操作日志
        /// </summary>
        public string SysOperationLog { get; set; } = "SysOperationLog";

        /// <summary>
        /// 消息通知
        /// </summary>
        public string UmsMessage { get; set; } = "UmsMessage";

        /// <summary>
        /// 微信客户消息
        /// </summary>
        public string WxgzhCustomerMsg { get; set; } = "WxgzhCustomerMsg";

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string WxgzhUnionId { get; set; } = "WxgzhUnionId";

        /// <summary>
        /// 微信菜单创建
        /// </summary>
        public string WxgzhMenu { get; set; } = "WxgzhMenu";
        
        /// <summary>
        /// 定时任务调度中心
        /// </summary>
        public string ScheduleJob { get; set; } = "ScheduleJob";
    }
}
