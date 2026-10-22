using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 系统通知-指定账号查询服务（只读）
    /// </summary>
    public interface ISysNotificationToAccountQueryService
    {
        /// <summary>
        /// 按消息id获取指定账号列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>账号列表</returns>
        Task<IEnumerable<SysNotificationToAccount>> GetListByMessageAsync(Guid messageId);
    }
}
