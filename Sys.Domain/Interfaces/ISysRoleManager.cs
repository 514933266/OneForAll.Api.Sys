﻿using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 领域服务：角色
    /// </summary>
    public interface ISysRoleManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>实体分页</returns>
        Task<PageList<SysRole>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid tenantId, SysRoleForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysRoleForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">角色id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

    }
}
