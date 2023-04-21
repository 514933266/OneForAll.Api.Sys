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
    /// 应用菜单
    /// </summary>
    public interface ISysMenuService
    {
        /// <summary>
        /// 获取树列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysMenuTreeDto>> GetListAsync(bool hasPerms, Guid parentId, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">菜单</param>
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
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysMenuForm entity);

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">菜单表单</param>
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id);

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="forms">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<SysMenuPermissionForm> forms);

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="form">权限菜单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdatePermissionAsync(Guid id, SysMenuPermissionForm form);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeletePermissionsAsync(Guid id, IEnumerable<Guid> permIds);
    }
}
