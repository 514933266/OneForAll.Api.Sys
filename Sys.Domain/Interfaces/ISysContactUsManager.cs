using OneForAll.Core;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 联系我们
    /// </summary>
    public interface ISysContactUsManager
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>、
        /// <returns>分页列表</returns>
        Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysContactUsForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
