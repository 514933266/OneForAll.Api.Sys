using OneForAll.Core;
using OneForAll.Core.Extension;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 领域服务：角色成员（命令：添加/移除）
    /// </summary>
    public class SysRoleMemberManager : SysDomainManager, ISysRoleMemberManager
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysMidRoleUserRepository _roleUserRepository;
        public SysRoleMemberManager(
            ICurrentUser currentUser,
            ISysRoleRepository roleRepository,
            ISysUserRepository userRepository,
            ISysMidRoleUserRepository roleUserRepository) : base(currentUser)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>权限列表</returns>
        public async Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> userIds)
        {
            var data = await _roleRepository.FindAsync(roleId);
            if (data == null)
                return BaseErrType.DataNotFound;

            var items = new List<SysMidRoleUser>();
            var users = await _userRepository.GetListAsync(userIds);
            var exists = await _roleUserRepository.GetListByRoleAsync(data.Id);
            users.ForEach(user =>
            {
                var item = exists.FirstOrDefault(w => w.SysUserId == user.Id);
                if (item == null)
                {
                    items.Add(new SysMidRoleUser()
                    {
                        SysRoleId = data.Id,
                        SysUserId = user.Id
                    });
                }
            });
            if (items.Any())
            {
                return await ResultAsync(() => _roleUserRepository.AddRangeAsync(items));
            }
            else
            {
                return BaseErrType.DataEmpty;
            }
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="userIds">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> RemoveAsync(Guid roleId, IEnumerable<Guid> userIds)
        {
            var data = await _roleRepository.FindAsync(roleId);
            if (data == null) return BaseErrType.DataNotFound;

            var users = await _roleUserRepository.GetListByRoleAsync(data.Id);
            if (users.Any())
            {
                return await ResultAsync(() => _roleUserRepository.DeleteRangeAsync(users));
            }
            else
            {
                return BaseErrType.DataEmpty;
            }
        }
    }
}
