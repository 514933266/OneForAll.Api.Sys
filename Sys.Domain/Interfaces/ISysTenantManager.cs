﻿using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;
using Sys.Domain.Aggregates;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    public interface ISysTenantManager
    {
        #region 租户
        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        Task<SysTenant> GetAsync(Guid id);

        /// <summary>
        /// 获取分页
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
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysTenantForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysTenantForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        #endregion

        #region 菜单

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysMenuPermissionAggr>> GetListMenuAsync(Guid id);

        #endregion

        #region 权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysPermission>> GetListPermissionAsync(Guid id);

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="entities">权限表单</param>
        /// <returns>结果</returns>

        Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<SysMenuPermissionForm> entities);
        
        #endregion

    }
}
