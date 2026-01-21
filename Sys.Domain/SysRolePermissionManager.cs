using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.EFCore;
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
    /// 领域服务：角色权限
    /// </summary>
    public class SysRolePermissionManager : BaseManager, ISysRolePermissionManager
    {
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysPermissionRepository _permRepository;
        private readonly ISysMidRolePermRepository _rolePermRepository;

        public SysRolePermissionManager(
            IHttpContextAccessor httpContextAccessor,
            ISysRoleRepository roleRepository,
            ISysPermissionRepository permRepository,
            ISysMidRolePermRepository rolePermRepository) : base(httpContextAccessor)
        {
            _roleRepository = roleRepository;
            _permRepository = permRepository;
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

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>权限列表</returns>
        public async Task<BaseErrType> AddAsync(Guid roleId, IEnumerable<Guid> permIds)
        {
            var data = await _roleRepository.FindAsync(roleId);
            if (data == null)
                return BaseErrType.DataNotFound;

            var addList = new List<SysMidRolePermission>();
            var permissions = await _permRepository.GetListAsync(permIds);
            var exists = await _rolePermRepository.GetListAsync(w => w.SysRoleId == data.Id);

            permissions.ForEach(perm =>
            {
                addList.Add(new SysMidRolePermission()
                {
                    SysPermissionId = perm.Id,
                    SysRoleId = roleId,
                });
            });

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                if (exists.Any())
                    await _rolePermRepository.DeleteRangeAsync(exists, tran);
                if (addList.Any())
                    await _rolePermRepository.AddRangeAsync(addList, tran);
            }
            return BaseErrType.DataNotFound;
        }
    }
}
