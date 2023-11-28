using Sys.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public interface ISysWxLoginUserService
    {
        /// <summary>
        /// 根据电话获取微信登录用户
        /// </summary>
        /// <param name="tenantId">机构id</param>
        /// <param name="mobile">手机号码</param>
        /// <returns>用户列表</returns>
        Task<SysWxLoginUserDto> GetByMobileAsync(Guid tenantId, string mobile);
    }
}
