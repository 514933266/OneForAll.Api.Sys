using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
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
    /// 微信公众号消息回复
    /// </summary>
    public class SysWxgzhReplySettingService : ISysWxgzhReplySettingService
    {
        private readonly IMapper _mapper;
        private readonly ISysWxgzhReplySettingManager _manager;
        private readonly ISysWxClientSettingManager _clientManager;

        public SysWxgzhReplySettingService(
            IMapper mapper,
            ISysWxgzhReplySettingManager manager,
            ISysWxClientSettingManager clientManager)
        {
            _mapper = mapper;
            _manager = manager;
            _clientManager = clientManager;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWxgzhReplySettingDto>> GetPageAsync(int pageIndex, int pageSize, string appId)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, appId);
            var items = _mapper.Map<IEnumerable<SysWxgzhReplySettingAggr>, IEnumerable<SysWxgzhReplySettingDto>>(data.Items);
            return new PageList<SysWxgzhReplySettingDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWxgzhReplySettingForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWxgzhReplySettingForm form)
        {
            return await _manager.UpdateAsync(form);
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
