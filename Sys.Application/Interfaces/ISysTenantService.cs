using Sys.Application.Dtos;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Enums;
using OneForAll.Core.Upload;
using System.IO;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 应用系统租户（租户）
    /// </summary>
    public interface ISysTenantService
    {
        #region 租户

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        Task<SysTenantDto> GetAsync(Guid id);

        /// <summary>
        /// 获取租户列表
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>租户</returns>
        Task<IEnumerable<SysTenantDto>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        Task<PageList<SysTenantDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            SysEnabledEnum isEnabled,
            DateTime? startDate,
            DateTime? endDate);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysTenantForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysTenantForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 上传Logo图片
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        Task<IUploadResult> UploadLogoAsync(Guid id, string filename, Stream file);

        #endregion

        #region 菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>菜单列表</returns>
        Task<IEnumerable<SysMenuTreeDto>> GetListMenuAsync(Guid id);

        #endregion

        #region 权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id);

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
