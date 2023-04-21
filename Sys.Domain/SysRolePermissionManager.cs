using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 领域服务：角色权限
    /// </summary>
    public class SysRolePermissionManager : BaseManager, ISysRolePermissionManager
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysPermissionRepository _permRepository;
        public SysRolePermissionManager(
            ISysRoleRepository roleRepository,
            ISysPermissionRepository permRepository)
        {
            _roleRepository = roleRepository;
            _permRepository = permRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListAsync(Guid roleId)
        {
            var role = await _roleRepository.GetWithPermsAsync(roleId);
            if (role != null)
            {
                return role
                    .SysRolePermContacts
                    .Select(s => s.SysPermission);
            }
            return new List<SysPermission>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>权限列表</returns>
        public async Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> permIds)
        {
            var data = await _roleRepository.GetWithPermsAsync(roleId);
            if (data != null)
            {
                var permissions = await _permRepository.GetListAsync(permIds);
                data.SysRolePermContacts.Clear();
                data.AddPermission(permissions);
                return await ResultAsync(() => _roleRepository.UpdateAsync(data));
            }
            return BaseErrType.DataNotFound;
        }
    }
}
