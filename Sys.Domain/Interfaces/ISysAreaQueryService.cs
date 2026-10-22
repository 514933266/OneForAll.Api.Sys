using Sys.Domain.Entities;
using OneForAll.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 地区查询服务（只读）
    /// </summary>
    public interface ISysAreaQueryService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysArea>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId);

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<SysArea>> GetListProvinceAsync();

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysArea>> GetChildrenAsync(int parentId);
    }
}
