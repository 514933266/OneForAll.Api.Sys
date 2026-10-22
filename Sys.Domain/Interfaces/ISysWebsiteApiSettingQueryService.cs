using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 网站设置-Api 查询服务（只读）
    /// </summary>
    public interface ISysWebsiteApiSettingQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWebsiteApiSetting>> GetListAsync(Guid settingId);
    }
}
