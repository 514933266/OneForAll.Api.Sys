using Sys.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface ISysMenuRepository : IEFCoreRepository<SysMenu>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="parentId">上级菜单id</param>
        /// <param name="key">关键字</param>
        /// <returns>实体</returns>
        Task<IEnumerable<SysMenu>> GetListAsync(Guid parentId, string key);

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysMenu>> GetListAsync(IEnumerable<Guid> ids);
    }
}
