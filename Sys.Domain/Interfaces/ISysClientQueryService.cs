using Sys.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 系统客户端查询服务（只读）
    /// </summary>
    public interface ISysClientQueryService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<SysClient>> GetListAsync();
    }
}
