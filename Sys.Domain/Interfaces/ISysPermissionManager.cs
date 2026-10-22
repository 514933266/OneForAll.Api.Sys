using Sys.Domain.Entities;
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
    /// 权限（命令）
    /// </summary>
    public interface ISysPermissionManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysPermissionForm entity);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="forms">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(IEnumerable<SysPermissionForm> forms);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysPermissionForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
