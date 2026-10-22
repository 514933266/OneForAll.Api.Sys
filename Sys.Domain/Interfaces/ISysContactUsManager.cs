using OneForAll.Core;
using Sys.Domain.Entities;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 联系我们（命令）
    /// </summary>
    public interface ISysContactUsManager
    {
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
