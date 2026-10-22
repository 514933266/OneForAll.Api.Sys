using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneForAll.Core.Security;
using Sys.Domain.Entities;
using System;

namespace Sys.Domain.Test.Entities
{
    /// <summary>
    /// SysUser 聚合行为单元测试（Phase 2 验证项）：
    /// 锁定 ApplyDefaultPassword（空密码→账号MD5 / 已有密码不覆盖）、
    /// SyncMobileFromUserName（账号为手机号且手机号空→回填 / 非手机号或已有手机不回填）两条领域规则不回归。
    /// </summary>
    [TestClass]
    public class SysUserBehaviorTests
    {
        // 构造一个可控 SysUser（仅填行为方法涉及的字段）
        private static SysUser NewUser(string userName, string password = null, string mobile = "")
        {
            return new SysUser
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Password = password,
                Mobile = mobile
            };
        }

        // ApplyDefaultPassword 分支1：密码为空 → 以账号 MD5 作为初始密码
        [TestMethod]
        public void ApplyDefaultPassword_EmptyPassword_SetsMd5OfUserName()
        {
            var user = NewUser("zhangsan", password: null);

            user.ApplyDefaultPassword();

            Assert.AreEqual("zhangsan".ToMd5(), user.Password);
            Assert.AreEqual(32, user.Password.Length); // MD5 十六进制
        }

        // ApplyDefaultPassword 分支2：已有密码 → 保持不变（不覆盖）
        [TestMethod]
        public void ApplyDefaultPassword_ExistingPassword_KeepsUnchanged()
        {
            var user = NewUser("zhangsan", password: "existinghash");

            user.ApplyDefaultPassword();

            Assert.AreEqual("existinghash", user.Password);
        }

        // SyncMobileFromUserName 分支1：账号为手机号且手机号为空 → 回填为账号
        [TestMethod]
        public void SyncMobileFromUserName_UserNameIsMobileAndMobileEmpty_Backfills()
        {
            var user = NewUser("13800000000", mobile: "");

            user.SyncMobileFromUserName();

            Assert.AreEqual("13800000000", user.Mobile);
        }

        // SyncMobileFromUserName 分支2：账号非手机号 → 不回填
        [TestMethod]
        public void SyncMobileFromUserName_UserNameNotMobile_DoesNotBackfill()
        {
            var user = NewUser("zhangsan", mobile: "");

            user.SyncMobileFromUserName();

            Assert.AreEqual("", user.Mobile);
        }

        // SyncMobileFromUserName 分支3：手机号已存在 → 保持不变（不覆盖）
        [TestMethod]
        public void SyncMobileFromUserName_MobileAlreadySet_KeepsUnchanged()
        {
            var user = NewUser("13800000000", mobile: "13900000000");

            user.SyncMobileFromUserName();

            Assert.AreEqual("13900000000", user.Mobile);
        }
    }
}
