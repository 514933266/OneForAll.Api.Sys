using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    public interface ISysTenantRepository : IEFCoreRepository<SysTenant>
    {
        /// <summary>
        /// 查询分页（含权限）a
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        Task<PageList<SysTenant>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<SysTenant>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询租户
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>结果</returns>
        Task<SysTenant> GetByNameAsync(string name);

        /// <summary>
        /// 查询租户
        /// </summary>
        /// <param name="code">租户信用代码</param>
        /// <returns>结果</returns>
        Task<SysTenant> GetByCodeAsync(string code);
    }
}
