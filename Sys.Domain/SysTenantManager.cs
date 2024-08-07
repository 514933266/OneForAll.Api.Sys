﻿using AutoMapper;
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
using OneForAll.EFCore;
using OneForAll.Core.Utility;
using OneForAll.Core.Extension;
using OneForAll.Core.Security;
using Sys.Domain.Enums;
using NPOI.POIFS.Properties;
using Sys.Domain.Aggregates;
using OneForAll.Core.Upload;
using OneForAll.File;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Sys.Domain
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    public class SysTenantManager: SysBaseManager, ISysTenantManager
    { 
        // 文件存储路径
        private readonly string UPLOAD_PATH = "/upload/logos/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "/resources/logos/";

        private readonly IUploader _uploader;
        private readonly ISysTenantRepository _repository;
        private readonly ISysPermissionRepository _permRepository;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysMenuRepository _menuRepository;
        private readonly ISysTenantPermContactRepository _tenantPermRepository;
        private readonly ISysUsertPermContactRepository _userPermRepository;
        private readonly ISysRolePermContactRepository _rolePermRepository;
        private readonly ISysRoleUserContactRepository _roleUserRepository;

        public SysTenantManager(
            IMapper mapper,
            IUploader uploader,
            IHttpContextAccessor httpContextAccessor,
            ISysTenantRepository repository,
            ISysPermissionRepository permRepository,
            ISysUserRepository userRepository,
            ISysRoleRepository roleRepository,
            ISysMenuRepository menuRepository,
            ISysTenantPermContactRepository tenantPermRepository,
            ISysUsertPermContactRepository userPermRepository,
            ISysRolePermContactRepository rolePermRepository,
            ISysRoleUserContactRepository roleUserRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
            _permRepository = permRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
            _tenantPermRepository = tenantPermRepository;
            _userPermRepository = userPermRepository;
            _rolePermRepository = rolePermRepository;
            _roleUserRepository = roleUserRepository;
        }

        #region 租户

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        public async Task<SysTenant> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        public async Task<PageList<SysTenant>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate)
        {
            if (pageIndex < 1)  pageIndex = 1;
            if (pageSize < 1)   pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key, isEnabled, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysTenantForm entity)
        {
            var data = await _repository.GetByNameAsync(entity.Name);
            if (data != null) return BaseErrType.DataExist;
            data = await _repository.GetByCodeAsync(entity.Code);
            if (data != null) return BaseErrType.DataExist;

            data = _mapper.Map<SysTenantForm, SysTenant>(entity);
            if (data.Code.IsNullOrEmpty())
            {
                data.Code = StringHelper.GetRandomString(18);
            }
            data.UpdateTime = DateTime.Now;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysTenantForm entity)
        {
            var data = await _repository.GetByNameAsync(entity.Name);
            if (data != null && data.Id != entity.Id) 
                return BaseErrType.DataExist;
            data = await _repository.GetByCodeAsync(entity.Code);
            if (data != null && data.Id != entity.Id) 
                return BaseErrType.DataExist;

            data = await _repository.FindAsync(entity.Id);
            data.MapFrom(entity);
            data.UpdateTime = DateTime.Now;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
                return BaseErrType.DataEmpty;
            var data = await _repository.GetListAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;
            if (ids.Count() > 1)
                return BaseErrType.Overflow;

            var id = ids.First();
            var perms = await _tenantPermRepository.GetListByTenantAsync(id);
            var users = await _userRepository.GetListByTenantAsync(id);
            var userPerms = await _userPermRepository.GetListByTenantAsync(id);
            var roles = await _roleRepository.GetListByTenantAsync(id);
            var rolePerms = await _rolePermRepository.GetListByTenantAsync(id);
            var roleUsers = await _roleUserRepository.GetListByTenantAsync(id);

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.DeleteRangeAsync(data, tran);
                if (perms.Any())
                    await _tenantPermRepository.DeleteRangeAsync(perms, tran);
                if (users.Any())
                    await _userRepository.DeleteRangeAsync(users, tran);
                if (userPerms.Any())
                    await _userRepository.DeleteRangeAsync(users, tran);
                if (roles.Any())
                    await _roleRepository.DeleteRangeAsync(roles, tran);
                if (rolePerms.Any())
                    await _rolePermRepository.DeleteRangeAsync(rolePerms, tran);
                if (roleUsers.Any())
                    await _roleUserRepository.DeleteRangeAsync(roleUsers, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 上传Logo
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        public async Task<IUploadResult> UploadLogoAsync(Guid id, string filename, Stream file)
        {
            var maxSize = 500 * 1024;
            var extension = Path.GetExtension(filename);

            var result = new UploadResult();
            var data = await _repository.FindAsync(id);
            if (data == null)
                return result;

            if (new ValidateImageType().Validate(filename, file))
            {
                var newfileName = id.ToString() + extension;
                var uploadPath = AppDomain.CurrentDomain.BaseDirectory + UPLOAD_PATH;
                var virtualPath = VIRTUAL_PATH;
                var host = _httpContextAccessor.HttpContext.Request.Scheme+ "://" + _httpContextAccessor.HttpContext.Request.Host.Value;

                result = await _uploader.WriteAsync(file, uploadPath, newfileName, maxSize) as UploadResult;
                // 设置返回虚拟路径
                if (result.State == UploadEnum.Success)
                {
                    result.Url = host + Path.Combine(virtualPath, newfileName);
                    data.LogoUrl = result.Url;
                    await _repository.UpdateAsync(data);
                    return result;
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }
        #endregion

        #region 菜单权限

        /// <summary>
        /// 获取可分配的菜单权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysMenuPermissionAggr>> GetListMenuAsync(Guid id)
        {
            var data = await _menuRepository.GetListAsync(Guid.Empty, string.Empty);
            var result = _mapper.Map<IEnumerable<SysMenu>, IEnumerable<SysMenuPermissionAggr>>(data);
            if (result.Any())
            {
                var ids = data.Select(s => s.Id).ToList();
                var perms = await _permRepository.GetListByMenuAsync(ids);
                result.ForEach(e =>
                {
                    e.SysPermissions = perms.Where(w => w.SysMenuId == e.Id).OrderBy(o => o.SortCode).ToList();
                });
            }
            return result;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListPermissionAsync(Guid id)
        {
            return await _permRepository.GetListByTenantAsync(id);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="pids">权限id</param>
        /// <returns>结果</returns>

        public async Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<Guid> pids)
        {
            if (!pids.Any()) return BaseErrType.DataEmpty;
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            var permissions = await _permRepository.GetListAsync(pids);
            var users = await _userRepository.GetListDefaultByTenantAsync(id);
            var tenantPerms = await _tenantPermRepository.GetListByTenantAsync(id);
            var userPerms = await _userPermRepository.GetListByTenantAsync(id);

            var addUserList = new List<SysUserPermContact>();
            var addList = permissions.Select(s => new SysTenantPermContact() { SysTenantId = id, SysPermissionId = s.Id }).ToList();

            users.ForEach(e =>
            {
                var list = permissions.Select(s => new SysUserPermContact() { SysUserId = e.Id, SysPermissionId = s.Id }).ToList();
                addUserList.AddRange(list);
            });
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                // 重置租户权限
                if (tenantPerms.Any())
                    await _tenantPermRepository.DeleteRangeAsync(tenantPerms, tran);
                if (addList.Any())
                    await _tenantPermRepository.AddRangeAsync(addList, tran);

                // 重置主管理员账号权限
                if (userPerms.Any())
                    await _userPermRepository.DeleteRangeAsync(userPerms, tran);
                if (addUserList.Any())
                    await _userPermRepository.AddRangeAsync(addUserList, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }
        #endregion
    }
}
