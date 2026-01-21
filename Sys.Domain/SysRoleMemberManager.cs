using Microsoft.AspNetCore.Http;
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

namespace Sys.Domain
{
    /// <summary>
    /// 领域服务：角色成员
    /// </summary>
    public class SysRoleMemberManager : BaseManager, ISysRoleMemberManager
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysMidRoleUserRepository _roleUserRepository;
        private readonly ISysMidRolePermRepository _rolePermRepository;
        public SysRoleMemberManager(
            IHttpContextAccessor httpContextAccessor,
            ISysRoleRepository roleRepository,
            ISysUserRepository userRepository,
            ISysMidRoleUserRepository roleUserRepository,
            ISysMidRolePermRepository rolePermRepository) : base(httpContextAccessor)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _roleUserRepository = roleUserRepository;
            _rolePermRepository = rolePermRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>权限列表</returns>
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

            var exists = await _roleUserRepository.GetListAsync(w => w.SysRoleId == data.Id);
            var existsIds = exists.Select(s => s.SysUserId);
            if (existsIds.Any())
            {
                return await _userRepository.GetListAsync(w => !existsIds.Contains(w.Id));
            }
            else
            {
                return await _userRepository.GetListAsync();
            }
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
            var exists = await _roleUserRepository.GetListAsync(w => w.SysRoleId == data.Id);
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

            var users = await _roleUserRepository.GetListAsync(w => w.SysRoleId == data.Id);
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
