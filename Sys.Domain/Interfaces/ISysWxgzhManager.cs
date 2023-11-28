using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public interface ISysWxgzhManager
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        Task<string> SubscribeEventAsync(string appId, string xmlContent);

        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        Task<string> MenuClickEventAsync(string appId, string xmlContent);

        /// <summary>
        /// 默认事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        Task<string> TextEventAsync(string appId, string xmlContent);

    }
}
