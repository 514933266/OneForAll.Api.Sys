using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 菜单
    /// </summary>
    public interface ISysMenuManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysMenuPermissionAggr>> GetListAsync(bool hasPerms, Guid parentId, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysMenuForm entity);

        /// <summary>
        /// 克隆
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="mids">克隆id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> CopyAsync(Guid id, IEnumerable<Guid> mids);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysMenuForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">实体</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns>结果</returns>
        Task<BaseErrType> EnableAsync(Guid id, bool isEnable);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="sortNumber">序号</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SortAsync(Guid id, int sortNumber);
    }
}
