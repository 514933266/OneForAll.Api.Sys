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
    /// 租户服务设置
    /// </summary>
    public interface ISysTenantServiceSettingRepository : IEFCoreRepository<SysTenantServiceSetting>
    {
        /// <summary>
        /// 查询服务已到期租户
        /// </summary>
        /// <returns>结果</returns>
        Task<IEnumerable<SysTenantServiceSetting>> GetListExpiryAsync();
    }
}
