using AutoMapper;
using OneForAll.Core.DDD;
using OneForAll.Core;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Domain
{
    /// <summary>
    /// 微信公众号消息回复
    /// </summary>
    public class SysWxgzhReplySettingManager : BaseManager, ISysWxgzhReplySettingManager
    {
        private readonly IMapper _mapper;
        private readonly ISysWxgzhReplySettingRepository _repository;
        public SysWxgzhReplySettingManager(
            IMapper mapper,
            ISysWxgzhReplySettingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWxgzhReplySettingAggr>> GetPageAsync(int pageIndex, int pageSize, string appId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, appId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWxgzhReplySettingForm form)
        {
            var exists = await _repository.CountAsync(w => w.AppId == form.AppId && w.MsgType == form.MsgType);
            if (exists > 0)
                return BaseErrType.DataExist;

            var data = _mapper.Map<SysWxgzhReplySettingForm, SysWxgzhReplySetting>(form);
            data.SetXmlContent();
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWxgzhReplySettingForm form)
        {
            var exists = await _repository.GetAsync(w => w.AppId == form.AppId && w.MsgType == form.MsgType);
            if (exists == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(exists, form);
            exists.SetXmlContent();
            return await ResultAsync(_repository.SaveChangesAsync);
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
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}

