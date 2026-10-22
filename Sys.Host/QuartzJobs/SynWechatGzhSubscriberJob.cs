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
    public class SynWechatGzhSubscriberJob : BaseLockJob
    {
        private readonly IWechatGzhHttpService _wxHttpService;
        private readonly ISysWechatUserRepository _userRepository;
        private readonly ISysWechatClientRepository _wxSettingRepository;
        private readonly ISysWechatGzhSubscriberRepository _subscribeUserRepository;

        public SynWechatGzhSubscriberJob(
            IWechatGzhHttpService wxHttpService,
            ISysWechatUserRepository userRepository,
            ISysWechatClientRepository wxSettingRepository,
            ISysWechatGzhSubscriberRepository subscribeUserRepository,
            QuartzScheduleJobConfig jobConfig,
            AuthConfig authConfig,
            JobLockConfig lockConfig,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
            : base(jobConfig, authConfig, lockConfig, jobHttpService, logHttpService)
        {
            _wxHttpService = wxHttpService;
            _userRepository = userRepository;
            _wxSettingRepository = wxSettingRepository;
            _subscribeUserRepository = subscribeUserRepository;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        protected override async Task ExecuteInternalAsync(IJobExecutionContext context)
        {
            var num = 0;
            var clients = await _wxSettingRepository.GetListAsync();
            var users = await _subscribeUserRepository.GetListUnboundAsync();
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
            var wxUsers = await _userRepository.GetListByUnionIdsAsync(uids);
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
            await AddLogAsync($"关联微信关注用户任务执行完成，共有{users.Count()}个关注用户，关联{num}个");
        }
    }
}
