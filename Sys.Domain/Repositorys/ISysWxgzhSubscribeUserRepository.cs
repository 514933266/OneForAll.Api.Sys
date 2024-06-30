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
    /// 微信公众号订阅用户
    /// </summary>
    public interface ISysWxgzhSubscribeUserRepository : IEFCoreRepository<SysWxgzhSubscribeUser>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWxgzhSubscribeUserAggr>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWxgzhSubscribeUserAggr>> GetListByUserAsync(IEnumerable<Guid> userIds);
    }
}
