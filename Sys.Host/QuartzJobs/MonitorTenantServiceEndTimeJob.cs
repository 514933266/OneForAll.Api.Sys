using Quartz;
using Sys.Application.Interfaces;
using Sys.Host.Models;
using Sys.HttpService.Interfaces;
using System.Threading.Tasks;

namespace Sys.Host.QuartzJobs
{
    /// <summary>
    /// 监控租户服务到期时间
    /// </summary>
    public class MonitorTenantServiceEndTimeJob : BaseLockJob
    {
        private readonly ISysTenantService _tenantService;

        public MonitorTenantServiceEndTimeJob(
            ISysTenantService tenantService,
            QuartzScheduleJobConfig jobConfig,
            AuthConfig authConfig,
            JobLockConfig lockConfig,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
            : base(jobConfig, authConfig, lockConfig, jobHttpService, logHttpService)
        {
            _tenantService = tenantService;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        protected override async Task ExecuteInternalAsync(IJobExecutionContext context)
        {
            var effected = await _tenantService.DisableExpiredServicesAsync();
            await AddLogAsync($"监控租户服务到期时间任务执行完成，共有{effected}个租户服务到期");
        }
    }
}
