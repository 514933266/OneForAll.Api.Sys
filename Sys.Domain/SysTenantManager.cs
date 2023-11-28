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
        private readonly ISysMenuRepository _menuRepository;
        private readonly ISysTenantPermContactRepository _contactRepository;
        private readonly ISysUsertPermContactRepository _userContactRepository;

        public SysTenantManager(
            IMapper mapper,
            IUploader uploader,
            IHttpContextAccessor httpContextAccessor,
            ISysTenantRepository repository,
            ISysPermissionRepository permRepository,
            ISysUserRepository userRepository,
            ISysMenuRepository menuRepository,
            ISysTenantPermContactRepository contactRepository,
            ISysUsertPermContactRepository userContactRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
            _permRepository = permRepository;
            _userRepository = userRepository;
            _menuRepository = menuRepository;
            _contactRepository = contactRepository;
            _userContactRepository = userContactRepository;
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
            if (data != null && data.Id != entity.Id) return BaseErrType.DataExist;
            data = await _repository.GetByCodeAsync(entity.Code);
            if (data != null && data.Id != entity.Id) return BaseErrType.DataExist;

            data = await _repository.FindAsync(entity.Id);
            data.MapFrom(entity);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (ids.Count() < 1) return BaseErrType.DataEmpty;
            var data = await _repository.GetListAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataEmpty;

            try
            {
                return await ResultAsync(() => _repository.DeleteRangeAsync(data));
            }
            catch
            {
                return BaseErrType.NotAllow;
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
        /// <param name="forms">权限表单</param>
        /// <returns>结果</returns>

        public async Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<SysMenuPermissionForm> forms)
        {
            if (!forms.Any())
                return BaseErrType.DataEmpty;
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            // 查出所有上级菜单的EnterView权限加入到选择中
            var menus = await _menuRepository.GetListAsync();
            var permissions = _mapper.Map<IEnumerable<SysMenuPermissionForm>, IEnumerable<SysPermission>>(forms);
            var ids = permissions.Select(s => new { s.Id, s.SysMenuId }).ToList();
            var mids = ids.Select(s => s.SysMenuId).ToList();
            var permMenus = FindAllMenus(mids, menus).DistinctBy(d => d.Id);
            mids = permMenus.Select(s => s.Id).ToList();
            var perms = await _permRepository.GetListByMenuAsync(mids);
            perms = permissions.Union(perms).DistinctBy(w => w.Id).ToList();

            var users = await _userRepository.GetListDefaultByTenantAsync(id);
            var tenantPerms = await _contactRepository.GetListByTenantAsync(id);
            var userPerms = await _userContactRepository.GetListByTenantAsync(id);

            var addUserList = new List<SysUserPermContact>();
            var addList = perms.Select(s => new SysTenantPermContact() { SysTenantId = id, SysPermissionId = s.Id }).ToList();

            users.ForEach(e =>
            {
                var list = perms.Select(s => new SysUserPermContact() { SysUserId = e.Id, SysPermissionId = s.Id }).ToList();
                addUserList.AddRange(list);
            });
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                // 重置租户权限
                if (tenantPerms.Any())
                    await _contactRepository.DeleteRangeAsync(tenantPerms, tran);
                if (addList.Any())
                    await _contactRepository.AddRangeAsync(addList, tran);

                // 重置主管理员账号权限
                if (userPerms.Any())
                    await _userContactRepository.DeleteRangeAsync(userPerms, tran);
                if (addUserList.Any())
                    await _userContactRepository.AddRangeAsync(addUserList, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        // 从下至上查找所有父级菜单
        private IEnumerable<SysMenu> FindAllMenus(IEnumerable<Guid> targetIds, IEnumerable<SysMenu> sources)
        {
            var result = new List<SysMenu>();
            var data = sources.Where(w => targetIds.Contains(w.Id)).ToList();
            if (data.Any())
            {
                result.AddRange(data);
                var pids = data.Select(s => s.ParentId).ToList();
                if (pids.Any())
                {
                    var parents = FindAllMenus(pids, sources);
                    if (parents.Any())
                        result.AddRange(parents);
                }
            }
            return result;
        }
        #endregion
    }
}
