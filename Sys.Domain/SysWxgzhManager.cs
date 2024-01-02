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
        private readonly ISysUserRepository _userRepository;
        private readonly ISysWechatUserRepository _wxuserRepository;
        private readonly ISysWxClientRepository _wxClientRepository;
        private readonly ISysWxgzhReplySettingRepository _replyRepository;
        private readonly ISysWxgzhSubscribeUserRepository _repository;

        private readonly IWxgzhHttpService _wxgzhHttpService;
        private readonly ISysGlobalExceptionLogHttpService _exHttpService;

        public SysWxgzhManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysUserRepository userRepository,
            ISysWechatUserRepository wxuserRepository,
            ISysWxgzhSubscribeUserRepository repository,
            ISysWxgzhReplySettingRepository replyRepository,
            ISysWxClientRepository wxClientRepository,
            IWxgzhHttpService wxgzhHttpService,
            ISysGlobalExceptionLogHttpService exHttpService) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _userRepository = userRepository;
            _wxuserRepository = wxuserRepository;
            _replyRepository = replyRepository;
            _wxClientRepository = wxClientRepository;
            _wxgzhHttpService = wxgzhHttpService;
            _exHttpService = exHttpService;
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
                var user = await _wxuserRepository.GetAsync(w => w.OpenId == data.FromUserName);
                var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.Subscribe);
                if (exists == null)
                {
                    var unionId = "";
                    var sysUserId = Guid.Empty;
                    if (user != null)
                    {
                        // 优先绑定公众号登录账号
                        unionId = user.UnionId;
                        sysUserId = user.SysUserId;
                    }
                    else
                    {
                        // 小程序账号
                        var client = await _wxClientRepository.GetAsync(w => w.AppId == appId);
                        if (client != null)
                        {
                            var unionRes = await _wxgzhHttpService.GetUnionIdAsync(data.FromUserName, client.AccessToken);
                            if (unionRes != null)
                            {
                                unionId = unionRes.UnionId ?? "";
                                user = await _wxuserRepository.GetAsync(w => w.UnionId == unionId);
                                if (user != null)
                                    sysUserId = user.SysUserId;
                            }
                        }
                    }
                    await _repository.AddAsync(new SysWxgzhSubscribeUser()
                    {
                        AppId = appId,
                        UnionId = unionId,
                        SysUserId = sysUserId,
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
                    await _repository.DeleteAsync(exists);
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
            var data = SerializationHelper.DeserializeXml<WxgzhTextEventForm>(xmlContent, Encoding.UTF8);
            if (data.Content.Contains("绑定账号"))
            {
                result = await BindSysAccount(appId, data);
            }
            else
            {
                var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.Text);
                if (msg != null)
                {
                    msg.ReplaceReplyTextContentJsonValue("抱歉，暂时无法理解您的问题");
                    result = msg.GetXmlReplyContent(data.FromUserName, data.ToUserName);
                }
            }
            return result;
        }

        // 绑定系统账号
        private async Task<string> BindSysAccount(string appId, WxgzhTextEventForm form)
        {
            var contentArr = form.Content.Split(':');
            var msg = await _replyRepository.GetAsync(w => w.AppId == appId && w.MsgType == SysWxgzhMsgTypeEnum.Text);
            var gzhUser = await _repository.GetAsync(w => w.AppId == appId && w.OpenId == form.FromUserName);
            if (gzhUser.SysUserId != Guid.Empty)
            {
                msg.ReplaceReplyTextContentJsonValue("请勿重复绑定账号");
            }
            else
            {
                if (gzhUser != null)
                {
                    var username = contentArr.Length > 1 ? contentArr[1] : "";
                    var user = await _userRepository.GetAsync(w => w.UserName == username);
                    if (user != null)
                    {
                        gzhUser.SysUserId = user.Id;
                        var effected = await _repository.SaveChangesAsync();
                        if (effected > 0)
                        {
                            msg.ReplaceReplyTextContentJsonValue("账号绑定成功！");
                        }
                        else
                        {
                            msg.ReplaceReplyTextContentJsonValue("账号绑定失败！");
                        }
                    }
                    else
                    {
                        msg.ReplaceReplyTextContentJsonValue("账号不存在");
                    }
                }
                else
                {
                    msg.ReplaceReplyTextContentJsonValue("请先关注公众号");
                }
            }

            if (msg != null)
                return msg.GetXmlReplyContent(form.FromUserName, form.ToUserName);

            return "";
        }
    }
}
