using AutoMapper;
using OneForAll.Core.DDD;
using OneForAll.Core;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public class SysContactUsManager : BaseManager, ISysContactUsManager
    {
        private readonly IMapper _mapper;
        private readonly ISysContactUsRepository _repository;
        public SysContactUsManager(
            IMapper mapper,
            ISysContactUsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysContactUsForm form)
        {
            var data = _mapper.Map<SysContactUsForm, SysContactUs>(form);
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
                return BaseErrType.DataEmpty;
            var data = await _repository.GetListAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}