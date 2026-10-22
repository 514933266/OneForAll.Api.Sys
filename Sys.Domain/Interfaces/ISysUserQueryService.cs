using OneForAll.Core;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 用户查询服务（只读）
    /// </summary>
    public interface ISysUserQueryService
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
        /// 按id集合获取用户
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetListByIdsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 按租户与手机号获取用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByMobileAsync(Guid tenantId, string mobile);

        /// <summary>
        /// 分页获取全部用户
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetPageAllAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 分页获取主账号用户
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <returns>用户列表</returns>
        Task<IEnumerable<SysUser>> GetPageMainAccountAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 按id获取用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByIdAsync(Guid id);

        /// <summary>
        /// 按id与租户获取用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="tenantId">机构id</param>
        /// <returns>用户</returns>
        Task<SysUser> GetByIdAndTenantAsync(Guid id, Guid tenantId);
    }
}
