using Quartz;
using Sys.Domain.Interfaces;
using Sys.Domain.Repositorys;
using Sys.Host.Models;
using Sys.HttpService.Interfaces;
using Sys.HttpService.Models;
using System.Threading.Tasks;
using System;
using Sys.Repository;
using System.Linq;

namespace Sys.Host.QuartzJobs
{
    /// <summary>
    /// 监控租户服务到期时间
    /// </summary>
    public class MonitorTenantServiceEndTimeJob : IJob
    {
        private readonly AuthConfig _config;
        
        private readonly ISysTenantRepository _tenantRepository;
        private readonly ISysTenantServiceSettingRepository _tenantSettingRepository;

        private readonly IScheduleJobHttpService _jobHttpService;
        private readonly ISysGlobalExceptionLogHttpService _logHttpService;

        public MonitorTenantServiceEndTimeJob(
            AuthConfig config,
            ISysTenantRepository tenantRepository,
            ISysTenantServiceSettingRepository tenantSettingRepository,
            IScheduleJobHttpService jobHttpService,
            ISysGlobalExceptionLogHttpService logHttpService)
        {
            _config = config;
            _tenantRepository = tenantRepository;
            _tenantSettingRepository = tenantSettingRepository;

            _jobHttpService = jobHttpService;
            _logHttpService = logHttpService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var effected = 0;
                var data = await _tenantSettingRepository.GetListExpiryAsync();
                if (data.Any())
                {
                    var ids = data.Select(s => s.Id).ToList();
                    var tenants = await _tenantRepository.GetListAsync(w => ids.Contains(w.Id));
                    foreach (var item in tenants)
                    {
                        item.IsEnabled = false;
                    }
                    effected = await _tenantRepository.SaveChangesAsync();
                }
                await _jobHttpService.LogAsync(_config.ClientCode, typeof(MonitorTenantServiceEndTimeJob).Name, $"监控租户服务到期时间任务执行完成，共有{effected}个租户服务到期");
            }
            catch (Exception ex)
            {
                await _logHttpService.AddAsync(new SysGlobalExceptionLogRequest
                {
                    MoudleName = _config.ClientName,
                    MoudleCode = _config.ClientCode,
                    Name = ex.Message,
                    Content = ex.InnerException == null ? ex.StackTrace : ex.InnerException.StackTrace
                });
            }
        }
    }
}
