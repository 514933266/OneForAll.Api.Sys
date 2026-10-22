using MediatR;
using Microsoft.Extensions.Logging;
using Sys.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Sys.Host.Events
{
    /// <summary>
    /// 通知发布事件订阅者：当前仅记录一条结构化审计日志（真实跨系统副作用出现时的扩展点，不臆造业务动作）。
    /// 领域事件（Sys.Domain）在实现 Core.IDomainEvent 的同时实现 INotification，故本订阅者直接实现 INotificationHandler&lt;TEvent&gt;。
    /// 经 Autofac 对宿主程序集 *Handler 的 AsImplementedInterfaces 扫描注册，供 IPublisher 按类型解析。
    /// </summary>
    public class NotificationPublishedHandler : INotificationHandler<NotificationPublishedEvent>
    {
        private readonly ILogger<NotificationPublishedHandler> _logger;

        public NotificationPublishedHandler(ILogger<NotificationPublishedHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(NotificationPublishedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "领域事件 NotificationPublishedEvent：NotificationId={NotificationId}, Title={Title}, EventId={EventId}, OccurredOn={OccurredOn}",
                notification.NotificationId, notification.Title, notification.EventId, notification.OccurredOn);
            return Task.CompletedTask;
        }
    }
}
