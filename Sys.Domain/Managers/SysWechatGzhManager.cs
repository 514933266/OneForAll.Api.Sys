using AutoMapper;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Entities;
using Sys.Domain.Enums;
using Sys.HttpService.Interfaces;
using OneForAll.Core.Extension;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    public class SysWechatGzhManager : SysDomainManager, ISysWechatGzhManager
    {
        private readonly IMapper _mapper;
        private readonly ISysUserRepository _userRepository;
        private readonly ISysWechatUserRepository _wxuserRepository;
        private readonly ISysWechatClientRepository _wxClientRepository;
        private readonly ISysWechatGzhReplySettingRepository _replyRepository;
        private readonly ISysWechatGzhSubscriberRepository _repository;

        private readonly IWechatGzhHttpService _wxgzhHttpService;
        private readonly ISysGlobalExceptionLogHttpService _exHttpService;

        public SysWechatGzhManager(
            IMapper mapper,
            ICurrentUser currentUser,
            ISysUserRepository userRepository,
            ISysWechatUserRepository wxuserRepository,
            ISysWechatGzhSubscriberRepository repository,
            ISysWechatGzhReplySettingRepository replyRepository,
            ISysWechatClientRepository wxClientRepository,
            IWechatGzhHttpService wxgzhHttpService,
            ISysGlobalExceptionLogHttpService exHttpService) : base(currentUser)
        {
            _mapper = mapper;
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
            var data = SerializationHelper.DeserializeXml<WechatGzhSubscribeEventForm>(xmlContent, Encoding.UTF8);
            var exists = await _repository.GetByOpenIdAsync(data.FromUserName);
            if (data.Event.ToLower() == "subscribe")
            {
                var subscribeType = data.EventKey.IsNullOrEmpty() ? SysWechatGzhSubscribeTypeEnum.Search : SysWechatGzhSubscribeTypeEnum.QRCode;
                var user = await _wxuserRepository.GetByOpenIdAsync(data.FromUserName);
                var msg = await _replyRepository.GetByAppIdMsgTypeAsync(appId, SysWechatGzhMsgTypeEnum.Subscribe);
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
                        var client = await _wxClientRepository.GetByAppIdAsync(appId);
                        if (client != null)
                        {
                            var unionRes = await _wxgzhHttpService.GetUnionIdAsync(data.FromUserName, client.AccessToken);
                            if (unionRes != null)
                            {
                                unionId = unionRes.UnionId ?? "";
                                user = await _wxuserRepository.GetByUnionIdAsync(unionId);
                                if (user != null)
                                    sysUserId = user.SysUserId;
                            }
                        }
                    }
                    await _repository.AddAsync(new SysWechatGzhSubscriber()
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
            var data = SerializationHelper.DeserializeXml<WechatGzhMenuClickEventForm>(xmlContent, Encoding.UTF8);
            var msg = await _replyRepository.GetByAppIdMsgTypeKeyAsync(appId, SysWechatGzhMsgTypeEnum.MenuClick, data.EventKey);
            if (msg != null)
            {
                if (msg.ReplyType == SysWechatGzhReplyTypeEnum.CustomerService)
                {
                    // 客服消息需要另外的接口进行发送
                    var client = await _wxClientRepository.GetByAppIdAsync(appId);
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
            var data = SerializationHelper.DeserializeXml<WechatGzhTextEventForm>(xmlContent, Encoding.UTF8);
            if (data.Content.Contains("绑定账号"))
            {
                result = await BindSysAccount(appId, data);
            }
            else
            {
                var msg = await _replyRepository.GetByAppIdMsgTypeAsync(appId, SysWechatGzhMsgTypeEnum.Text);
                if (msg != null)
                {
                    msg.ReplaceReplyTextContentJsonValue("抱歉，暂时无法理解您的问题");
                    result = msg.GetXmlReplyContent(data.FromUserName, data.ToUserName);
                }
            }
            return result;
        }

        // 绑定系统账号
        private async Task<string> BindSysAccount(string appId, WechatGzhTextEventForm form)
        {
            var contentArr = form.Content.Split(':');
            var msg = await _replyRepository.GetByAppIdMsgTypeAsync(appId, SysWechatGzhMsgTypeEnum.Text);
            var gzhUser = await _repository.GetByAppIdOpenIdAsync(appId, form.FromUserName);
            if (gzhUser.SysUserId != Guid.Empty)
            {
                msg.ReplaceReplyTextContentJsonValue("请勿重复绑定账号");
            }
            else
            {
                if (gzhUser != null)
                {
                    var username = contentArr.Length > 1 ? contentArr[1] : "";
                    var user = await _userRepository.GetAsync(username);
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

        /// <summary>
        /// 关联微信关注用户（补全UnionId并绑定系统用户）
        /// </summary>
        /// <returns>关联结果</returns>
        public async Task<SysWechatGzhSubscriberSynResult> SynSubscribersAsync()
        {
            var result = new SysWechatGzhSubscriberSynResult();
            var clients = (await _wxClientRepository.GetListAsync())?.ToList() ?? new List<SysWechatClient>();
            var users = (await _repository.GetListUnboundAsync())?.ToList() ?? new List<SysWechatGzhSubscriber>();
            result.TotalCount = users.Count;
            if (users.Count == 0) return result;

            foreach (var user in users)
            {
                var client = clients.FirstOrDefault(w => w.AppId == user.AppId);
                if (client == null)
                    continue;
                if (user.UnionId.IsNullOrEmpty())
                {
                    var data = await _wxgzhHttpService.GetUnionIdAsync(user.OpenId, client.AccessToken);
                    if (data != null)
                        user.UnionId = data.UnionId ?? "";
                }
            }

            var unionIds = users.Where(w => !w.UnionId.IsNullOrEmpty()).Select(s => s.UnionId).ToList();
            if (!unionIds.Any()) return result;

            var wxUsers = (await _wxuserRepository.GetListByUnionIdsAsync(unionIds))?.ToList() ?? new List<SysWechatUser>();
            users.ForEach(e =>
            {
                var wxUser = wxUsers.FirstOrDefault(w => w.UnionId == e.UnionId);
                if (wxUser != null)
                {
                    e.SysUserId = wxUser.SysUserId;
                    e.UnionId = wxUser.UnionId ?? "";
                }
            });
            result.UpdatedCount = await _repository.SaveChangesAsync();
            return result;
        }
    }
}
