using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 网站设置-Api 查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysWebsiteApiSettingQueryService : ISysWebsiteApiSettingQueryService
    {
        private readonly ISysWebsiteApiSettingRepository _repository;

        public SysWebsiteApiSettingQueryService(ISysWebsiteApiSettingRepository repository)
        {
            _repository = repository;
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
    }
}
