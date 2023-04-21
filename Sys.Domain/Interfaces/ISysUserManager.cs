using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface ISysUserManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        Task<PageList<SysUserAggr>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysUserForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysUserForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
