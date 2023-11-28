using Sys.Application.Dtos;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 应用权限
    /// </summary>
    public interface ISysPermissionService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysPermissionDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            Guid menuId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysPermissionForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysPermissionForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
