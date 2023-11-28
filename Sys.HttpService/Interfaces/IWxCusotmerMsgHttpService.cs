using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.HttpService.Interfaces
{
    /// <summary>
    /// 微信客服消息
    /// </summary>
    public interface IWxCusotmerMsgHttpService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="jsonStr">发送的json内容</param>
        /// <param name="accessToken">访问令牌</param>
        /// <returns></returns>
        Task SendAsync(string jsonStr, string accessToken);
    }
}
