using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 领域服务：角色
    /// </summary>
    public class SysRoleManager : BaseManager, ISysRoleManager
    {
        private readonly IMapper _mapper;
        private readonly ISysRoleRepository _roleRepository;
        public SysRoleManager(
            IMapper mapper,
            ISysRoleRepository roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>角色分页</returns>
        public async Task<PageList<SysRole>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1)  pageIndex = 1;
            if (pageSize < 1)   pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _roleRepository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid tenantId, SysRoleForm entity)
        {
            var data = await _roleRepository.GetByNameAsync(entity.Name);
            if (data != null) return BaseErrType.DataExist;

            data = _mapper.Map<SysRoleForm, SysRole>(entity);
            data.Id = Guid.NewGuid();
            data.SysTenantId = tenantId;
            return await ResultAsync(()=> _roleRepository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysRoleForm entity)
        {
            var data = await _roleRepository.GetByNameAsync(entity.Name);
            if (data != null && data.Id != entity.Id) return BaseErrType.DataExist;

            data = await _roleRepository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            data.MapFrom(entity);
            return await ResultAsync(() => _roleRepository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _roleRepository.GetListAsync(ids);
            return await ResultAsync(() => _roleRepository.DeleteRangeAsync(data));
        }
    }
}
