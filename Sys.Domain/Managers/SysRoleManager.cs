using AutoMapper;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 领域服务：角色（命令：增删改）
    /// </summary>
    public class SysRoleManager : BaseManager, ISysRoleManager
    {
        private readonly IMapper _mapper;
        private readonly ISysRoleRepository _roleRepository;
        public SysRoleManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysRoleRepository roleRepository) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
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

            _mapper.Map(entity,data);
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
