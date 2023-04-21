using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 网站设置
    /// </summary>
    public interface ISysWebsiteSettingRepository : IEFCoreRepository<SysWebsiteSetting>
    {
        #region 列表

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysWebsiteSetting>> GetListAsTrackingAsync(IEnumerable<Guid> ids);

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>用户列表</returns>
        Task<SysWebsiteSetting> GetByHostAsync(string host);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>实体</returns>
        Task<SysWebsiteSettingAggr> GetByHostWithContactAsync(string host);

        #endregion
    }
}
