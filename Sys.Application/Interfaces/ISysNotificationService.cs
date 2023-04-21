using OneForAll.Core;
using OneForAll.Core.Upload;
using Sys.Application.Dtos;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public interface ISysNotificationService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysNotificationDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysNotificationForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysNotificationForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file);

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> PublishAsync(Guid id);
    }
}
