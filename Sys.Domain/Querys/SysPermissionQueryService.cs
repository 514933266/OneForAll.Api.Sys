using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 权限查询服务（只读，返回聚合根，不含 DTO）
    /// </summary>
    public class SysPermissionQueryService : ISysPermissionQueryService
    {
        private readonly ISysPermissionRepository _repository;

        public SysPermissionQueryService(ISysPermissionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysPermissionAggr>> GetPageAsync(int pageIndex, int pageSize, string key, Guid menuId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageWithMenuAsync(pageIndex, pageSize, key, menuId);
        }

        /// <summary>
        /// 获取菜单下的权限列表
        /// </summary>
        /// <param name="menuId">菜单id</param>
        /// <returns>权限列表</returns>
        public async Task<IEnumerable<SysPermission>> GetListByMenuAsync(Guid menuId)
        {
            return await _repository.GetListByMenuAsync(menuId);
        }
    }
}
