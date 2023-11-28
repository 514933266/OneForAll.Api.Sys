using AutoMapper;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 租户选项
    /// </summary>
    public class SysTenantSelectionService : ISysTenantSelectionService
    {
        private readonly IMapper _mapper;
        private readonly ISysTenantRepository _repository;
        public SysTenantSelectionService(
            IMapper mapper,
            ISysTenantRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<SysTenantSelectionDto> GetAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            return _mapper.Map<SysTenant, SysTenantSelectionDto>(data);
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysTenantSelectionDto>> GetListAsync(string key)
        {
            var predicate = PredicateBuilder.Create<SysTenant>(w => true);
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.Name.Contains(key));
            var data = await _repository.GetListAsync(predicate);
            return _mapper.Map<IEnumerable<SysTenant>, IEnumerable<SysTenantSelectionDto>>(data);
        }
    }
}
