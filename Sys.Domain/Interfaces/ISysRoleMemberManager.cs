using Sys.Domain.Entities;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 领域服务：角色成员（命令）
    /// </summary>
    public interface ISysRoleMemberManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>权限列表</returns>
        Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> userIds);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> RemoveAsync(Guid roleId, IEnumerable<Guid> userIds);
    }
}
