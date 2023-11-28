using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    /// 微信客户端
    /// </summary>
    public class SysWxClientSettingManager : SysBaseManager, ISysWxClientSettingManager
    {
        private readonly ISysWxClientSettingRepository _repository;

        public SysWxClientSettingManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWxClientSettingRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWxClientSetting>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWxClientSettingForm form)
        {
            var exists = await _repository.CountAsync(w => w.AppId == form.AppId);
            if (exists > 0)
                return BaseErrType.DataExist;

            var data = _mapper.Map<SysWxClientSettingForm, SysWxClientSetting>(form);
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWxClientSettingForm form)
        {
            var exists = await _repository.GetAsync(w => w.AppId == form.AppId);
            if (exists == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(exists, form);
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
