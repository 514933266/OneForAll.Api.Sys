using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Security;
using Microsoft.AspNetCore.Http;

namespace Sys.Domain
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public class SysWebsiteApiSettingManager : SysBaseManager, ISysWebsiteApiSettingManager
    {
        private readonly ISysWebsiteApiSettingRepository _repository;
        private readonly ISysWebsiteSettingRepository _settingRepository;

        public SysWebsiteApiSettingManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWebsiteApiSettingRepository repository,
            ISysWebsiteSettingRepository settingRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _settingRepository = settingRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWebsiteApiSetting>> GetListAsync(Guid settingId)
        {
            return await _repository.GetListAsync(settingId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid settingId, SysWebsiteApiSettingForm entity)
        {
            var setting = await _settingRepository.FindAsync(settingId);
            if (setting == null) return BaseErrType.DataError;

            var data = _mapper.Map<SysWebsiteApiSettingForm, SysWebsiteApiSetting>(entity);
            data.SysWebsiteSettingId = setting.Id;

            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid settingId, SysWebsiteApiSettingForm entity)
        {
            var setting = await _settingRepository.FindAsync(settingId);
            if (setting == null) return BaseErrType.DataError;

            var data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(entity, data);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="settingId">设置id</param>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid settingId, IEnumerable<Guid> ids)
        {
            var setting = await _settingRepository.FindAsync(settingId);
            if (setting == null) return BaseErrType.DataError;

            var data = await _repository.GetListAsTrackingAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataNotFound;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}