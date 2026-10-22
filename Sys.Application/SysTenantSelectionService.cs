using AutoMapper;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
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
        private readonly ISysTenantQueryService _query;
        public SysTenantSelectionService(
            IMapper mapper,
            ISysTenantQueryService query)
        {
            _mapper = mapper;
            _query = query;
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户列表</returns>
        public async Task<SysTenantSelectionDto> GetAsync(Guid id)
        {
            var data = await _query.GetAsync(id);
            return _mapper.Map<SysTenant, SysTenantSelectionDto>(data);
        }

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        public async Task<IEnumerable<SysTenantSelectionDto>> GetListAsync(string key)
        {
            var data = await _query.GetListByKeyAsync(key);
            return _mapper.Map<IEnumerable<SysTenant>, IEnumerable<SysTenantSelectionDto>>(data);
        }
    }
}
