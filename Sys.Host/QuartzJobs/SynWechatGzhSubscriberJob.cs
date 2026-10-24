using Quartz;
using Sys.Application.Interfaces;
using Sys.Host.Models;
using Sys.HttpService.Interfaces;
using System.Threading.Tasks;

namespace Sys.Host.QuartzJobs
{
    /// <summary>
    /// 关联微信公众号关注用户
    /// </summary>
    public class SynWechatGzhSubscriberJob : BaseLockJob
    {
        private readonly ISysWechatGzhSubscriberService _subscriberService;

        public SynWechatGzhSubscriberJob(
            ISysWechatGzhSubscriberService subscriberService,
            QuartzScheduleJobConfig jobConfig,
            AuthConfig authConfig,
            JobLockConfig lockConfig,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
            : base(jobConfig, authConfig, lockConfig, jobHttpService, logHttpService)
        {
            _subscriberService = subscriberService;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        protected override async Task ExecuteInternalAsync(IJobExecutionContext context)
        {
            var result = await _subscriberService.SynSubscribersAsync();
            await AddLogAsync($"关联微信关注用户任务执行完成，共有{result.TotalCount}个关注用户，关联{result.UpdatedCount}个");
        }
    }
}
