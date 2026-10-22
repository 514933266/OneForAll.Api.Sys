using OneForAll.Core;
using Sys.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 系统通知查询服务（只读）
    /// </summary>
    public interface ISysNotificationQueryService
    {
        /// <summary>
        /// 获取分页
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
        /// 按id获取通知
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>通知</returns>
        Task<SysNotification> GetAsync(Guid id);
    }
}
