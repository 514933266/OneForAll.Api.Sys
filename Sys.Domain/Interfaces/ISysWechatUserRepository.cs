using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    public interface ISysWechatUserRepository : IEFCoreRepository<SysWechatUser>
    {
        /// <summary>
        /// 按手机号查询实体
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>微信用户</returns>
        Task<SysWechatUser> GetByMobileAsync(string mobile);

        /// <summary>
        /// 按UnionId查询实体
        /// </summary>
        /// <param name="unionId">UnionId</param>
        /// <returns>微信用户</returns>
        Task<SysWechatUser> GetByUnionIdAsync(string unionId);

        /// <summary>
        /// 按OpenId查询实体
        /// </summary>
        /// <param name="openId">OpenId</param>
        /// <returns>微信用户</returns>
        Task<SysWechatUser> GetByOpenIdAsync(string openId);

        /// <summary>
        /// 按UnionId集合查询列表
        /// </summary>
        /// <param name="unionIds">UnionId集合</param>
        /// <returns>微信用户列表</returns>
        Task<IEnumerable<SysWechatUser>> GetListByUnionIdsAsync(List<string> unionIds);
    }
}
