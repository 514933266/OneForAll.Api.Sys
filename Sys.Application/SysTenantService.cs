using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Extension;
using Sys.Domain.Enums;
using System.Runtime.InteropServices;
using Sys.Domain.Repositorys;
using Sys.Domain.Aggregates;
using NPOI.SS.Formula.Functions;
using OneForAll.Core.ORM;
using OneForAll.Core.Upload;
using System.IO;

namespace Sys.Application
{
    /// <summary>
    /// 应用租户（租户）
    /// </summary>
    public class SysTenantService : ISysTenantService
    {
        private readonly IMapper _mapper;
        private readonly ISysTenantManager _manager;
        public SysTenantService(
            IMapper mapper,
            ISysTenantManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        #region 租户

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        public async Task<SysTenantDto> GetAsync(Guid id)
        {
            var data = await _manager.GetAsync(id);
            return _mapper.Map<SysTenant, SysTenantDto>(data);
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
        public async Task<PageList<SysTenantDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key, isEnabled, startDate, endDate);
            var items = _mapper.Map<IEnumerable<SysTenant>, IEnumerable<SysTenantDto>>(data.Items);
            return new PageList<SysTenantDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysTenantForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysTenantForm form)
        {
            return await _manager.UpdateAsync(form);
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
        /// 上传Logo图片
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        public async Task<IUploadResult> UploadLogoAsync(Guid id, string filename, Stream file)
        {
            return await _manager.UploadLogoAsync(id, filename, file);
        }

        #endregion

        #region 菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>菜单列表</returns>
        public async Task<IEnumerable<SysMenuTreeDto>> GetListMenuAsync(Guid id)
        {
            var data = await _manager.GetListMenuAsync(id);
            var menus = _mapper.Map<IEnumerable<SysMenuPermissionAggr>, IEnumerable<SysMenuTreeDto>>(data);
            return menus.ToTree<SysMenuTreeDto, Guid>();
        }

        #endregion

        #region 权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id)
        {
            var data = await _manager.GetListPermissionAsync(id);
            return _mapper.Map<IEnumerable<SysPermission>, IEnumerable<SysMenuPermissionDto>>(data);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="entities">权限表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<SysMenuPermissionForm> entities)
        {
            return await _manager.AddPermissionAsync(id, entities);
        }
        #endregion
    }
}
