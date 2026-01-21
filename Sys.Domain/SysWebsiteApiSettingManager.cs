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
using Sys.Domain.Enums;

namespace Sys.Domain
{
    /// <summary>
    /// 网站设置-Api
    /// </summary>
    public class SysWebsiteApiSettingManager : BaseManager, ISysWebsiteApiSettingManager
    {
        private readonly IMapper _mapper;
        private readonly ISysWebsiteApiSettingRepository _repository;
        private readonly ISysWebsiteSettingRepository _settingRepository;

        public SysWebsiteApiSettingManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWebsiteApiSettingRepository repository,
            ISysWebsiteSettingRepository settingRepository) : base(httpContextAccessor)
        {
            _mapper = mapper;
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

            var data = await _repository.GetListAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataNotFound;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 根据远程组件菜单创建API列表
        /// </summary>
        /// <param name="tenantId">租户id</param>
        /// <param name="menus">菜单列表</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CreateByMenuAsync(Guid tenantId, IEnumerable<SysMenu> menus)
        {
            var data = await _repository.GetListByTenantAsync(tenantId);
            menus = menus.Where(w => w.Type == SysMenuTypeEnum.Node).ToList();

            return BaseErrType.Success;
        }
    }
}