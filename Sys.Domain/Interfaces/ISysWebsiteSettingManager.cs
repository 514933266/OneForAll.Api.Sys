using OneForAll.Core;
using OneForAll.Core.UploadFile;
using Sys.Domain.Entities;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 网站设置（命令）
    /// </summary>
    public interface ISysWebsiteSettingManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysWebsiteSettingForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysWebsiteSettingForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file);
    }
}
