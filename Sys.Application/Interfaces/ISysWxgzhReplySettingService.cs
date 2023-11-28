using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 微信公众号消息回复
    /// </summary>
    public interface ISysWxgzhReplySettingService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWxgzhReplySettingDto>> GetPageAsync(int pageIndex, int pageSize, string appId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysWxgzhReplySettingForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysWxgzhReplySettingForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
