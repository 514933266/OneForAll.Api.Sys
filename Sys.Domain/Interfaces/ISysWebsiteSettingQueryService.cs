using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using OneForAll.Core;
using System;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 网站设置查询服务（只读）
    /// </summary>
    public interface ISysWebsiteSettingQueryService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<SysWebsiteSetting> GetAsync(Guid id);

        /// <summary>
        /// 按域名获取实体（含联系方式聚合）
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>聚合</returns>
        Task<SysWebsiteSettingAggr> GetByHostWithContactAsync(string host);
    }
}
