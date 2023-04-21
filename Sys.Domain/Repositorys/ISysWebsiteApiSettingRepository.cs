using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public interface ISysWebsiteApiSettingRepository : IEFCoreRepository<SysWebsiteApiSetting>
    {
        #region 列表

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysWebsiteApiSetting>> GetListAsync(Guid settingId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysWebsiteApiSetting>> GetListAsTrackingAsync(IEnumerable<Guid> ids);

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="host">域名</param>
        /// <returns>用户列表</returns>
        Task<SysWebsiteApiSetting> GetByHostAsync(Guid settingId, string host);

        #endregion
    }
}
