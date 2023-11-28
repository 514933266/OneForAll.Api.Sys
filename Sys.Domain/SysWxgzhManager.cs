using AutoMapper;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.AggregateRoots;
using Microsoft.IdentityModel.Tokens;
using Sys.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Sys.HttpService.Interfaces;
using Newtonsoft.Json.Linq;

namespace Sys.Domain
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public class SysWxgzhManager : SysBaseManager, ISysWxgzhManager
    {
        private readonly ISysWechatUserRepository _userRepository;
        private readonly ISysWxClientSettingRepository _wxClientRepository;
        private readonly ISysWxgzhReplySettingRepository _replyRepository;
        private readonly ISysWxgzhSubscribeUserRepository _repository;

        private readonly IWxgzhHttpService _wxgzhHttpService;

        public SysWxgzhManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWechatUserRepository userRepository,
            ISysWxgzhSubscribeUserRepository repository,
            ISysWxgzhReplySettingRepository replyRepository,
            ISysWxClientSettingRepository wxClientRepository,
            IWxgzhHttpService wxgzhHttpService) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _userRepository = userRepository;
            _replyRepository = replyRepository;
            _wxClientRepository = wxClientRepository;
            _wxgzhHttpService = wxgzhHttpService;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        public async Task<string> SubscribeEventAsync(string appId, string xmlContent)
        {
            var result = "";
            var data = SerializationHelper.DeserializeXml<WxgzhSubscribeEventForm>(xmlContent, Encoding.UTF8);
            var exists = await _repository.GetAsync(w => w.OpenId == data.FromUserName);
            if (data.Event.ToLower() == "subscribe")
            {
                var subscribeType = data.EventKey.IsNullOrEmpty() ? SysWxgzhSubscribeType.Search : SysWxgzhSubscribeType.QRCode;
                var user = await _userRepository.GetAsync(w => w.OpenId == data.FromUserName);
                var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.Subscribe);
                if (exists == null)
                {
                    await _repository.AddAsync(new SysWxgzhSubscribeUser()
                    {
                        AppId = appId,
                        SysUserId = user?.Id ?? Guid.Empty,
                        OpenId = data.FromUserName,
                        IsUnSubscribed = false,
                        SubscribeType = subscribeType
                    });
                }
                else
                {
                    // 当用户不关注时，删除关注数据
                    await _repository.DeleteAsync(exists);
                }

                if (msg != null)
                    result = msg.GetXmlReplyContent(data.FromUserName, data.ToUserName);
            }
            else
            {
                if (exists != null)
                {
                    exists.IsUnSubscribed = true;
                    await _repository.SaveChangesAsync();
                }
            }
            return result;
        }


        /// <summary>
        /// 菜单点击事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        public async Task<string> MenuClickEventAsync(string appId, string xmlContent)
        {
            var result = "";
            var data = SerializationHelper.DeserializeXml<WxgzhMenuClickEventForm>(xmlContent, Encoding.UTF8);
            var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.MenuClick && w.MsgKey == data.EventKey);
            if (msg != null)
            {
                if (msg.ReplyType == SysWxgzhReplyTypeEnum.CustomerService)
                {
                    // 客服消息需要另外的接口进行发送
                    var client = await _wxClientRepository.GetAsync(w => w.AppId == appId);
                    if (client != null)
                    {
                        var list = msg.GetJsonReplyContent<object>(data.FromUserName, data.ToUserName);
                        list.ForEach(e =>
                        {
                            _wxgzhHttpService.SendCustomerMessageAsync(e.ToString(), client.AccessToken);
                        });
                    }
                }
                else
                {
                    result = msg.GetXmlReplyContent(data.FromUserName, data.ToUserName);
                }
            }
            return result;
        }

        /// <summary>
        /// 默认事件
        /// </summary>
        /// <param name="appId">所属公众号应用id</param>
        /// <param name="xmlContent">事件内容</param>
        /// <returns></returns>
        public async Task<string> TextEventAsync(string appId, string xmlContent)
        {
            var result = "";
            var data = SerializationHelper.DeserializeXml<WxgzhMenuClickEventForm>(xmlContent, Encoding.UTF8);
            var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.Text);
            if (msg != null)
            {
                result = msg.GetXmlReplyContent(data.FromUserName, data.ToUserName);
            }
            return result;
        }
    }
}
