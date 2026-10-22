using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 网站设置查询服务（只读，返回实体/聚合，不含 DTO）
    /// </summary>
    public class SysWebsiteSettingQueryService : ISysWebsiteSettingQueryService
    {
        private readonly ISysWebsiteSettingRepository _repository;

        public SysWebsiteSettingQueryService(ISysWebsiteSettingRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        public async Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSetting> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        /// <summary>
        /// 按域名获取实体（含联系方式聚合）
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>聚合</returns>
        public async Task<SysWebsiteSettingAggr> GetByHostWithContactAsync(string host)
        {
            return await _repository.GetByHostWithContactAsync(host);
        }
    }
}
