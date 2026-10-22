using Sys.Domain.Entities;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;
using Sys.Domain.Aggregates;
using System.IO;
using OneForAll.Core.UploadFile;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    public interface ISysTenantManager
    {
        #region 租户

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysTenantForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysTenantForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传Logo
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        Task<IUploadResult> UploadLogoAsync(Guid id, string filename, Stream file);

        #endregion

        #region 权限

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="pids">权限id</param>
        /// <returns>结果</returns>

        Task<BaseErrType> AddPermissionAsync(Guid id, IEnumerable<Guid> pids);

        #endregion

    }
}
