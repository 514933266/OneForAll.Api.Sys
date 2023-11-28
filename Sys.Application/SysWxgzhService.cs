using AutoMapper;
using Sys.Application.Interfaces;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using Sys.HttpService.Interfaces;
using OneForAll.Core;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public class SysWxgzhService : ISysWxgzhService
    {
        private readonly ISysWxgzhManager _manager;
        public SysWxgzhService(ISysWxgzhManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 用户事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">微信xml事件内容</param>
        /// <returns></returns>
        public async Task<string> UserEventAsync(string appId, string xmlContent)
        {
            var data = SerializationHelper.DeserializeXml<WxgzhEventForm>(xmlContent, Encoding.UTF8);
            var type = data.MsgType.ToLower();
            var eventType = data.Event.ToLower();
            switch (type)
            {
                case "event":
                    if (eventType == "subscribe" || eventType == "unsubscribe")
                    {
                        return await _manager.SubscribeEventAsync(appId, xmlContent);
                    }
                    else if (eventType == "click")
                    {
                        return await _manager.MenuClickEventAsync(appId, xmlContent);
                    }
                    break;
                case "text":
                    return await _manager.TextEventAsync(appId, xmlContent);
            }
            return "";
        }
    }
}
