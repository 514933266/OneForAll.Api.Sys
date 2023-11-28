using Sys.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 微信公众号关注用户
    /// </summary>
    public interface ISysWxgzhNotifyUserService
    {
        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端id</param>
        /// <returns>用户</returns>
        Task<SysWxgzhNotifyUserDto> GetAsync(Guid userId, string clientId);
    }
}
