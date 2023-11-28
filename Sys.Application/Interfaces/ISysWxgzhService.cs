using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public interface ISysWxgzhService
    {
        /// <summary>
        /// 用户事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">微信xml事件内容</param>
        /// <returns></returns>
        Task<string> UserEventAsync(string appId, string xmlContent);
    }
}
