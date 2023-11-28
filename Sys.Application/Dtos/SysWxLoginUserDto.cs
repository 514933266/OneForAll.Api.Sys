using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    public class SysWxLoginUserDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string IconUrl { get; set; }
    }
}
