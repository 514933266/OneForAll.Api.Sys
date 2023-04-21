using OneForAll.Core;
using OneForAll.Core.Upload;
using Sys.Domain.AggregateRoots;
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
    /// 网站设置
    /// </summary>
    public interface ISysWebsiteSettingManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<SysWebsiteSetting> GetAsync(Guid id);

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
