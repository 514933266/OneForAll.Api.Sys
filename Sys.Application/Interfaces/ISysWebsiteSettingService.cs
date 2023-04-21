using Sys.Application.Dtos;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Upload;
using System.IO;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 应用权限
    /// </summary>
    public interface ISysWebsiteSettingService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWebsiteSettingDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<SysWebsiteSettingDto> GetAsync(Guid id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>实体</returns>
        Task<SysWebsiteSettingDto> GetAsync(string host);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysWebsiteSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">权限</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysWebsiteSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传楼盘图片
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file);

        #region Api授权设置

        /// <summary>
        /// 获取api设置列表
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<SysWebsiteApiSettingDto>> GetListApiAsync(Guid id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddApiAsync(Guid id, SysWebsiteApiSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateApiAsync(Guid id, SysWebsiteApiSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="ids">api设置id</param>
        /// <returns>消息</returns>
        Task<BaseErrType> DeleteApiAsync(Guid id, IEnumerable<Guid> ids);

        #endregion
    }
}
