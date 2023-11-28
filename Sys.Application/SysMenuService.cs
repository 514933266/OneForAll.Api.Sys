using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;
using Sys.Domain.Repositorys;

namespace Sys.Application
{
    /// <summary>
    /// 应用菜单
    /// </summary>
    public class SysMenuService : ISysMenuService
    {
        private readonly IMapper _mapper;
        private readonly ISysMenuManager _manager;
        private readonly ISysPermissionManager _permManager;
        private readonly ISysPermissionRepository _permRepository;

        public SysMenuService(
            IMapper mapper,
            ISysMenuManager manager,
            ISysPermissionManager permManager,
            ISysPermissionRepository permRepository
            )
        {
            _mapper = mapper;
            _manager = manager;
            _permManager = permManager;
            _permRepository = permRepository;
        }

        #region 菜单

        /// <summary>
        /// 获取树列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysMenuTreeDto>> GetListAsync(bool hasPerms, Guid parentId, string key)
        {
            var data = await _manager.GetListAsync(hasPerms, parentId, key);
            var menus = _mapper.Map<IEnumerable<SysMenuPermissionAggr>, IEnumerable<SysMenuTreeDto>>(data);
            return menus.ToTree<SysMenuTreeDto, Guid>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysMenuForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="mids">克隆id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CopyAsync(Guid id, IEnumerable<Guid> mids)
        {
            return await _manager.CopyAsync(id, mids);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysMenuForm form)
        {
            return await _manager.UpdateAsync(form);
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> EnableAsync(Guid id, bool isEnable)
        {
            return await _manager.EnableAsync(id, isEnable);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="sortNumber">序号</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(Guid id, int sortNumber)
        {
            return await _manager.SortAsync(id, sortNumber);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id)
        {
            var data = await _permRepository.GetListByMenuAsync(id);
            return _mapper.Map<IEnumerable<SysPermission>, IEnumerable<SysMenuPermissionDto>>(data);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="forms">权限</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<SysMenuPermissionForm> forms)
        {
            var data = _mapper.Map<IEnumerable<SysMenuPermissionForm>, IEnumerable<SysPermissionForm>>(forms);
            data.ForEach(e => e.MenuId = id);
            return await _permManager.AddAsync(data);
        }


        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="form">权限表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdatePermissionAsync(Guid id, SysMenuPermissionForm form)
        {
            var data = _mapper.Map<SysMenuPermissionForm, SysPermissionForm>(form);
            data.MenuId = id;
            return await _permManager.UpdateAsync(data);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeletePermissionsAsync(Guid id, IEnumerable<Guid> permIds)
        {
            return await _permManager.DeleteAsync(permIds);
        }
        #endregion
    }
}
