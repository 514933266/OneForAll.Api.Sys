using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public class SysWxClientSettingDto
    {
        /// <summary>
        /// 实体id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 系统客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 系统客户端密码
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 系统客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 微信客户端id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信客户端密码
        /// </summary>
        public string AppSecret { get; set; }
    }
}
