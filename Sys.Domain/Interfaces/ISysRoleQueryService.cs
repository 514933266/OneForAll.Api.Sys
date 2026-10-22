using Sys.Domain.Entities;
using OneForAll.Core;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 角色查询服务（只读）
    /// </summary>
    public interface ISysRoleQueryService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>实体分页</returns>
        Task<PageList<SysRole>> GetPageAsync(int pageIndex, int pageSize, string key);
    }
}
