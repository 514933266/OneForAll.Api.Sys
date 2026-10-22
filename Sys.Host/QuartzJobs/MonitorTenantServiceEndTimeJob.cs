using Quartz;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
using Sys.Host.Models;
using Sys.HttpService.Interfaces;
using System.Threading.Tasks;
using Sys.Repository;
using System.Linq;

namespace Sys.Host.QuartzJobs
{
    /// <summary>
    /// 监控租户服务到期时间
    /// </summary>
    public class MonitorTenantServiceEndTimeJob : BaseLockJob
    {
        private readonly ISysTenantRepository _tenantRepository;
        private readonly ISysTenantServiceSettingRepository _tenantSettingRepository;

        public MonitorTenantServiceEndTimeJob(
            ISysTenantRepository tenantRepository,
            ISysTenantServiceSettingRepository tenantSettingRepository,
            QuartzScheduleJobConfig jobConfig,
            AuthConfig authConfig,
            JobLockConfig lockConfig,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
            : base(jobConfig, authConfig, lockConfig, jobHttpService, logHttpService)
        {
            _tenantRepository = tenantRepository;
            _tenantSettingRepository = tenantSettingRepository;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        protected override async Task ExecuteInternalAsync(IJobExecutionContext context)
        {
            var effected = 0;
            var data = await _tenantSettingRepository.GetListExpiryAsync();
            if (data.Any())
            {
                var ids = data.Select(s => s.Id).ToList();
                var tenants = await _tenantRepository.GetListAsync(ids);
                foreach (var item in tenants)
                {
                    item.IsEnabled = false;
                }
                effected = await _tenantRepository.SaveChangesAsync();
            }
            await AddLogAsync($"监控租户服务到期时间任务执行完成，共有{effected}个租户服务到期");
        }
    }
}
