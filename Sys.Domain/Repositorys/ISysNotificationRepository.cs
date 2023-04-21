using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public interface ISysNotificationRepository : IEFCoreRepository<SysNotification>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysNotification>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysNotification>> GetListAsTrackingAsync(IEnumerable<Guid> ids);
    }
}
