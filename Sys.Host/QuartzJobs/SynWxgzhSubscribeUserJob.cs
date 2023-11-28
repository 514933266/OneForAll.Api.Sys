using Quartz;
using Sys.Host.Models;
using System.Threading.Tasks;
using Sys.Domain.Repositorys;
using Sys.HttpService.Interfaces;
using System;
using System.Linq;
using NPOI.XSSF.Model;
using Microsoft.IdentityModel.Tokens;
using Sys.Domain.Interfaces;
using OneForAll.Core.Extension;

namespace Sys.Host.QuartzJobs
{
    /// <summary>
    /// 关联微信公众号关注用户
    /// </summary>
    public class SynWxgzhSubscribeUserJob : IJob
    {
        private readonly AuthConfig _config;
        private readonly IWxgzhHttpService _wxHttpService;
        private readonly IScheduleJobHttpService _jobHttpService;
        private readonly ISysWechatUserRepository _userRepository;
        private readonly ISysWxClientSettingRepository _wxSettingRepository;
        private readonly ISysWxgzhSubscribeUserRepository _subscribeUserRepository;

        public SynWxgzhSubscribeUserJob(
            AuthConfig config,
            IWxgzhHttpService wxHttpService,
            IScheduleJobHttpService jobHttpService,
            ISysWechatUserRepository userRepository,
            ISysWxClientSettingRepository wxSettingRepository,
            ISysWxgzhSubscribeUserRepository subscribeUserRepository)
        {
            _config = config;
            _wxHttpService = wxHttpService;
            _jobHttpService = jobHttpService;
            _userRepository = userRepository;
            _wxSettingRepository = wxSettingRepository;
            _subscribeUserRepository = subscribeUserRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var num = 0;
            var clients = await _wxSettingRepository.GetListAsync();
            var users = await _subscribeUserRepository.GetListAsync(w => w.SysUserId == Guid.Empty);
            foreach (var user in users)
            {
                var client = clients.FirstOrDefault(w => w.AppId == user.AppId);
                if (client == null)
                    continue;
                if (user.UnionId.IsNullOrEmpty())
                {
                    var data = await _wxHttpService.GetUnionIdAsync(user.OpenId, client.AccessToken);
                    if (data != null)
                        user.UnionId = data.UnionId ?? "";
                }
            }

            var uids = users.Where(w => !w.UnionId.IsNullOrEmpty()).Select(s => s.UnionId).ToList();
            var wxUsers = await _userRepository.GetListAsync(w => uids.Contains(w.UnionId));
            users.ForEach(e =>
            {
                var wxUser = wxUsers.FirstOrDefault(w => w.UnionId == e.UnionId);
                if (wxUser != null)
                {
                    e.SysUserId = wxUser.SysUserId;
                    e.UnionId = wxUser.UnionId ?? "";
                }
            });
            num = await _subscribeUserRepository.SaveChangesAsync();
            await _jobHttpService.LogAsync(_config.ClientCode, typeof(SynWxgzhSubscribeUserJob).Name, $"关联微信关注用户任务执行完成，共有{users.Count()}个关注用户，关联{num}个");
        }
    }
}
