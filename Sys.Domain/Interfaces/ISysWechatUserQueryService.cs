using Sys.Domain.Entities;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信登录用户查询服务（只读）
    /// </summary>
    public interface ISysWechatUserQueryService
    {
        /// <summary>
        /// 按手机号获取微信登录用户
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>微信登录用户</returns>
        Task<SysWechatUser> GetByMobileAsync(string mobile);

        /// <summary>
        /// 按UnionId获取微信登录用户
        /// </summary>
        /// <param name="unionId">微信unionId</param>
        /// <returns>微信登录用户</returns>
        Task<SysWechatUser> GetByUnionIdAsync(string unionId);
    }
}
