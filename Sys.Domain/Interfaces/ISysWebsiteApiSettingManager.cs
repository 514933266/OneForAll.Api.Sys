using OneForAll.Core;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public interface ISysWebsiteApiSettingManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWebsiteApiSetting>> GetListAsync(Guid settingId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid settingId, SysWebsiteApiSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid settingId, SysWebsiteApiSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid settingId, IEnumerable<Guid> ids);
    }
}
