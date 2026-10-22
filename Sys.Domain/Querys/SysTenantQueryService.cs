using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Enums;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 租户查询服务（只读，返回实体/聚合根，不含 DTO）
    /// </summary>
    public class SysTenantQueryService : ISysTenantQueryService
    {
        private readonly IMapper _mapper;
        private readonly ISysTenantRepository _repository;
        private readonly ISysMenuRepository _menuRepository;
        private readonly ISysPermissionRepository _permRepository;

        public SysTenantQueryService(
            IMapper mapper,
            ISysTenantRepository repository,
            ISysMenuRepository menuRepository,
            ISysPermissionRepository permRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _menuRepository = menuRepository;
            _permRepository = permRepository;
        }

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
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key, isEnabled, startDate, endDate);
        }

        /// <summary>
        /// 按id集合获取租户
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysTenant>> GetListByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _repository.GetListAsync(ids);
        }

        /// <summary>
        /// 按关键字获取租户（下拉选项）
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysTenant>> GetListByKeyAsync(string key)
        {
            return await _repository.GetListByKeyAsync(key);
        }

        /// <summary>
        /// 获取可分配的菜单权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>菜单权限列表</returns>
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
    }
}
