using MediatR;
using OneForAll.Core.Events;
using System;

namespace Sys.Domain.Events
{
    /// <summary>
    /// 通知发布事件（单命令语义：通知成功发布后由 Manager 发布）。
    /// 同时实现 Core 的纯抽象 IDomainEvent 与 MediatR 的 INotification，
    /// 便于宿主层 IPublisher 按运行时类型派发到对应 INotificationHandler&lt;TEvent&gt;。
    /// </summary>
    public class NotificationPublishedEvent : IDomainEvent, INotification
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.Now;

        /// <summary>
        /// 通知id
        /// </summary>
        public Guid NotificationId { get; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; }

        public NotificationPublishedEvent(Guid notificationId, string title)
        {
            NotificationId = notificationId;
            Title = title;
        }
    }
}
