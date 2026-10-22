using Sys.Domain.Entities;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 领域服务：角色权限（命令）
    /// </summary>
    public interface ISysRolePermissionManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>权限列表</returns>
        Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> permIds);
    }
}
