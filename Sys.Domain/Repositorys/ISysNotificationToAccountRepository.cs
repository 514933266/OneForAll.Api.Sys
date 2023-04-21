using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 系统通知-指定账号
    /// </summary>
    public interface ISysNotificationToAccountRepository : IEFCoreRepository<SysNotificationToAccount>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysNotificationToAccount>> GetListAsync(Guid messageId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysNotificationToAccount>> GetListAsTrackingAsync(Guid messageId);
    }
}
