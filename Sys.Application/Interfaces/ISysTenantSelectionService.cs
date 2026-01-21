using Sys.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 租户选项
    /// </summary>
    public interface ISysTenantSelectionService
    {
        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户列表</returns>
        Task<SysTenantSelectionDto> GetAsync(Guid id);

        /// <summary>
        /// 获取租户下拉列表
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>租户列表</returns>
        Task<IEnumerable<SysTenantSelectionDto>> GetListAsync(string key);
    }
}
