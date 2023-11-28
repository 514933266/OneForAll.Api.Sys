using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 应用用户
    /// </summary>
    public interface ISysUserService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        Task<PageList<SysUserDto>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysUserForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysUserForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
