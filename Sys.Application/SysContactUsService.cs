using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// /// <summary>
    /// 联系我们
    /// </summary>
    /// </summary>
    public class SysContactUsService : ISysContactUsService
    {
        private readonly IMapper _mapper;
        private readonly ISysContactUsManager _manager;
        public SysContactUsService(
            IMapper mapper,
            ISysContactUsManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysContactUsDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key);
            var items = _mapper.Map<IEnumerable<SysContactUs>, IEnumerable<SysContactUsDto>>(data.Items);
            return new PageList<SysContactUsDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysContactUsForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }
    }
}
