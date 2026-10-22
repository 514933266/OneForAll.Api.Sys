using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core;
using Sys.Domain.Entities;
using System;

namespace Sys.Domain.Test.Entities
{
    /// <summary>
    /// SysNotification 聚合行为单元测试（Phase 2 验证项）：
    /// 锁定 Publish（未发布→Success 且置已发布 / 已发布→NotAllow 幂等）、
    /// ResetToDraft（撤回发布态）两条领域规则不回归。
    /// </summary>
    [TestClass]
    public class SysNotificationBehaviorTests
    {
        // 构造一个可控 SysNotification（仅填行为方法涉及的字段）
        private static SysNotification NewNotification(bool isPublish = false)
        {
            return new SysNotification
            {
                Id = Guid.NewGuid(),
                Title = "系统公告",
                Content = "内容",
                IsPublish = isPublish
            };
        }

        // Publish 分支1：未发布 → 返回 Success 且置为已发布
        [TestMethod]
        public void Publish_NotPublished_ReturnsSuccessAndMarksPublished()
        {
            var notification = NewNotification(isPublish: false);

            var result = notification.Publish();

            Assert.AreEqual(BaseErrType.Success, result);
            Assert.IsTrue(notification.IsPublish);
        }

        // Publish 分支2：已发布 → 返回 NotAllow 且状态保持已发布（幂等）
        [TestMethod]
        public void Publish_AlreadyPublished_ReturnsNotAllowAndStaysPublished()
        {
            var notification = NewNotification(isPublish: true);

            var result = notification.Publish();

            Assert.AreEqual(BaseErrType.NotAllow, result);
            Assert.IsTrue(notification.IsPublish);
        }

        // Publish 连续调用：第一次 Success，第二次 NotAllow（发布不可重复）
        [TestMethod]
        public void Publish_CalledTwice_IsIdempotentReject()
        {
            var notification = NewNotification(isPublish: false);

            Assert.AreEqual(BaseErrType.Success, notification.Publish());
            Assert.AreEqual(BaseErrType.NotAllow, notification.Publish());
            Assert.IsTrue(notification.IsPublish);
        }

        // ResetToDraft：已发布 → 撤回为草稿（IsPublish=false）
        [TestMethod]
        public void ResetToDraft_Published_ResetsToDraft()
        {
            var notification = NewNotification(isPublish: true);

            notification.ResetToDraft();

            Assert.IsFalse(notification.IsPublish);
        }
    }
}
