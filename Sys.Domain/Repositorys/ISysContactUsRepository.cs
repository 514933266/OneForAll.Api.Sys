using OneForAll.Core;
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
    /// 联系我们
    /// </summary>
    public interface ISysContactUsRepository : IEFCoreRepository<SysContactUs>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysContactUs>> GetListAsync(IEnumerable<Guid> ids);
    }
}
