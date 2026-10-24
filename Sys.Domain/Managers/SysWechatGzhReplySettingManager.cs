using AutoMapper;
using OneForAll.Core;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 微信公众号消息回复（命令：增删改）
    /// </summary>
    public class SysWechatGzhReplySettingManager : SysDomainManager, ISysWechatGzhReplySettingManager
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatGzhReplySettingRepository _repository;
        public SysWechatGzhReplySettingManager(
            IMapper mapper,
            ICurrentUser currentUser,
            ISysWechatGzhReplySettingRepository repository):base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWechatGzhReplySettingForm form)
        {
            var exists = await _repository.ExistsByAppIdMsgTypeAsync(form.AppId, form.MsgType);
            if (exists)
                return BaseErrType.DataExist;

            var data = _mapper.Map<SysWechatGzhReplySettingForm, SysWechatGzhReplySetting>(form);
            data.SetXmlContent();
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWechatGzhReplySettingForm form)
        {
            var exists = await _repository.GetByAppIdMsgTypeAsync(form.AppId, form.MsgType);
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
            var data = await _repository.GetListByIdsAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}
