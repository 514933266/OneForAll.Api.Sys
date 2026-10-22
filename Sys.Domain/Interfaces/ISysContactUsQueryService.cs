using Sys.Domain.Entities;
using OneForAll.Core;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 联系我们查询服务（只读）
    /// </summary>
    public interface ISysContactUsQueryService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key);
    }
}
