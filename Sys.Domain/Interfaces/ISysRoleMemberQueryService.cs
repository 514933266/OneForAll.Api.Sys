using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 角色成员查询服务（只读）
    /// </summary>
    public interface ISysRoleMemberQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>成员列表</returns>
        Task<IEnumerable<SysUser>> GetListAsync(Guid roleId);

        /// <summary>
        /// 获取未加入实体的成员列表
        /// </summary>
        /// <param name="roleId">实体id</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetListUnJoinedAsync(Guid roleId, string key);
    }
}
