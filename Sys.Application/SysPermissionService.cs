using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 应用权限
    /// </summary>
    public class SysPermissionService : ISysPermissionService
    {
        private readonly IMapper _mapper;
        private readonly ISysPermissionManager _permManager;
        public SysPermissionService(
            IMapper mapper,
            ISysPermissionManager permManager)
        {
            _mapper = mapper;
            _permManager = permManager;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysPermissionDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            Guid menuId)
        {
            var data = await _permManager.GetPageAsync(pageIndex, pageSize, key, menuId);
            var items = _mapper.Map<IEnumerable<SysPermission>, IEnumerable<SysPermissionDto>>(data.Items);
            return new PageList<SysPermissionDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysPermissionForm form)
        {
            return await _permManager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysPermissionForm form)
        {
            return await _permManager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _permManager.DeleteAsync(ids);
        }
    }
}
