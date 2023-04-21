using Sys.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 地区
    /// </summary>
    public interface ISysAreaRepository : IEFCoreRepository<SysArea>
    {
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysArea>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<SysArea>> GetListAsync(IEnumerable<int> ids);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns>实体</returns>
        Task<SysArea> GetByCodeAsync(string code);

        /// <summary>
        /// 查询子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysArea>> GetChildrenAsync(int parentId);
    }
}
