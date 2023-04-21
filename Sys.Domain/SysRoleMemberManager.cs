using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 领域服务：角色成员
    /// </summary>
    public class SysRoleMemberManager : BaseManager, ISysRoleMemberManager
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysUserRepository _userRepository;
        public SysRoleMemberManager(
            ISysRoleRepository roleRepository,
            ISysUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysUser>> GetListAsync(Guid roleId)
        {
            var data = await _roleRepository.GetWithMembersAsync(roleId);
            if (data != null)
            {
                return data.SysRoleUserContacts.Select(s => s.SysUser);
            }
            return new List<SysUser>();
        }

        /// <summary>
        /// 获取未加入实体的成员列表
        /// </summary>
        /// <param name="roleId">实体id</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<SysUser>> GetListUnJoinedAsync(Guid roleId, string key)
        {
            var data = await _roleRepository.GetWithMembersAsync(roleId);
            if (data != null)
            {
                var users = await _userRepository.GetListAsync();
                var existsIds = data.SysRoleUserContacts.Select(s => s.SysUserId);
                return users.Where(w => w.Id.NotIn(existsIds));
            }
            return new List<SysUser>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>权限列表</returns>
        public async Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> userIds)
        {
            var data = await _roleRepository.GetWithMembersAsync(roleId);
            if (data == null) return BaseErrType.DataNotFound;

            var users = await _userRepository.GetListAsync(userIds);
            data.AddMember(users);
            return await ResultAsync(() => _roleRepository.UpdateAsync(data));
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> RemoveAsync(Guid roleId, IEnumerable<Guid> userIds)
        {
            var data = await _roleRepository.GetWithMembersAsync(roleId);
            if (data == null) return BaseErrType.DataNotFound;

            data.RemoveMember(userIds);
            return await ResultAsync(() => _roleRepository.UpdateAsync(data));
        }
    }
}
