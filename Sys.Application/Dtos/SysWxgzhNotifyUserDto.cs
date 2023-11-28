using Sys.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Dtos
{
    /// <summary>
    /// 微信关注用户
    /// </summary>
    public class SysWxgzhNotifyUserDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 微信用户OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 系统用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 关注方式
        /// </summary>
        public SysWxgzhSubscribeType SubscribeType { get; set; }

        /// <summary>
        /// 是否已取消订阅
        /// </summary>
        public bool IsUnSubscribed { get; set; }

        /// <summary>
        /// 客户端的AccessToken
        /// </summary>
        public string AppAccessToken { get; set; }
    }
}
