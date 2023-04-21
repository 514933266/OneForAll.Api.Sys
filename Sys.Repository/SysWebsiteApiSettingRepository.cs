using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public class SysWebsiteApiSettingRepository : Repository<SysWebsiteApiSetting>, ISysWebsiteApiSettingRepository
    {
        public SysWebsiteApiSettingRepository(DbContext context)
            : base(context)
        {

        }

        #region 列表

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysWebsiteApiSetting>> GetListAsync(Guid settingId)
        {
            return await DbSet
                .Where(w => w.SysWebsiteSettingId == settingId)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysWebsiteApiSetting>> GetListAsTrackingAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
        }

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="host">域名</param>
        /// <returns>用户列表</returns>
        public async Task<SysWebsiteApiSetting> GetByHostAsync(Guid settingId, string host)
        {
            return await DbSet
                .Where(w => w.SysWebsiteSettingId == settingId && w.Host == host)
                .FirstOrDefaultAsync();
        }

        #endregion
    }
}
