using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 角色权限查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysRolePermissionQueryService : ISysRolePermissionQueryService
    {
        private readonly ISysMidRolePermRepository _rolePermRepository;

        public SysRolePermissionQueryService(ISysMidRolePermRepository rolePermRepository)
        {
            _rolePermRepository = rolePermRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListAsync(Guid roleId)
        {
            return await _rolePermRepository.GetListPermissionAsync(roleId);
        }
    }
}
