using OneForAll.Core;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 系统通知查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysNotificationQueryService : ISysNotificationQueryService
    {
        private readonly ISysNotificationRepository _repository;

        public SysNotificationQueryService(ISysNotificationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysNotification>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key, startDate, endDate);
        }

        /// <summary>
        /// 按id获取通知
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>通知</returns>
        public async Task<SysNotification> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }
    }
}
