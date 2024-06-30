using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
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
    public interface ISysWxgzhSubscribeUserService
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户</returns>
        Task<PageList<SysWxgzhSubscribeUserDto>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取微信公众号关注用户列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>用户</returns>
        Task<IEnumerable<SysWxgzhSubscribeUserDto>> GetListAsync([FromBody] IEnumerable<Guid> userIds);

        /// <summary>
        /// 获取微信公众号关注用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="clientId">客户端id</param>
        /// <returns>用户</returns>
        Task<SysWxgzhSubscribeUserTokenDto> GetAsync(Guid userId, string clientId);
    }
}
