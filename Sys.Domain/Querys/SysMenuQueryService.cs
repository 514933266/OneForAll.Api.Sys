using AutoMapper;
using OneForAll.Core.Extension;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 菜单查询服务（只读，返回聚合根，不含 DTO）
    /// </summary>
    public class SysMenuQueryService : ISysMenuQueryService
    {
        private readonly IMapper _mapper;
        private readonly ISysMenuRepository _repository;
        private readonly ISysPermissionRepository _permRepository;

        public SysMenuQueryService(
            IMapper mapper,
            ISysMenuRepository repository,
            ISysPermissionRepository permRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _permRepository = permRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysMenuPermissionAggr>> GetListAsync(bool hasPerms, Guid parentId, string key)
        {
            var data = await _repository.GetListAsync(parentId, key);
            var result = _mapper.Map<IEnumerable<SysMenu>, IEnumerable<SysMenuPermissionAggr>>(data);
            if (hasPerms)
            {
                // 查询带权限时，只显示启用的菜单
                result = result.Where(w => w.IsEnabled).ToList();
                var ids = data.Select(s => s.Id).ToList();
                var perms = await _permRepository.GetListByMenuAsync(ids);
                result.ForEach(e =>
                {
                    e.SysPermissions = perms.Where(w => w.SysMenuId == e.Id).OrderByDescending(o => o.SortCode).ToList();
                });
            }
            return result;
        }
    }
}
