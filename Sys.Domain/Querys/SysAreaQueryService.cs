using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 地区查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysAreaQueryService : ISysAreaQueryService
    {
        private readonly ISysAreaRepository _areaRepository;

        public SysAreaQueryService(ISysAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysArea>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId)
        {
            return await _areaRepository.GetPageAsync(pageIndex, pageSize, key, parentId);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysArea>> GetListProvinceAsync()
        {
            return await _areaRepository.GetChildrenAsync(0);
        }

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysArea>> GetChildrenAsync(int parentId)
        {
            return await _areaRepository.GetChildrenAsync(parentId);
        }
    }
}
