using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 角色成员查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysRoleMemberQueryService : ISysRoleMemberQueryService
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysMidRoleUserRepository _roleUserRepository;

        public SysRoleMemberQueryService(
            ISysRoleRepository roleRepository,
            ISysUserRepository userRepository,
            ISysMidRoleUserRepository roleUserRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>成员列表</returns>
        public async Task<IEnumerable<SysUser>> GetListAsync(Guid roleId)
        {
            return await _roleUserRepository.GetListUserAsync(roleId);
        }

        /// <summary>
        /// 获取未加入实体的成员列表
        /// </summary>
        /// <param name="roleId">实体id</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetListUnJoinedAsync(Guid roleId, string key)
        {
            var data = await _roleRepository.FindAsync(roleId);
            if (data == null)
                return Enumerable.Empty<SysUser>();

            var exists = await _roleUserRepository.GetListByRoleAsync(data.Id);
            var existsIds = exists.Select(s => s.SysUserId);
            if (existsIds.Any())
            {
                return await _userRepository.GetListNotInIdsAsync(existsIds);
            }
            else
            {
                return await _userRepository.GetListAsync();
            }
        }
    }
}
