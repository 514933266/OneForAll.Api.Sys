using System;
using Sys.Domain;
using Sys.Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sys.Host
{
    public partial class OneForAllContext : DbContext
    {
        public OneForAllContext(DbContextOptions<OneForAllContext> options)
            : base(options)
        {

        }

        public virtual DbSet<SysService> SysBaseService { get; set; }

        #region 菜单权限

        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<SysTenantPermContact> SysTenantPermContact { get; set; }
        public virtual DbSet<SysUserPermContact> SysUserPermContact { get; set; }
        public virtual DbSet<SysRolePermContact> SysRolePermContact { get; set; }
        public virtual DbSet<SysRoleUserContact> SysRoleUserContact { get; set; }

        #endregion

        #region 系统用户

        public virtual DbSet<SysTenant> SysTenant { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysClient> SysClient { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        
        #endregion

        #region 公共数据

        public virtual DbSet<SysArea> SysArea { get; set; }

        #endregion

        #region 网站设置

        public virtual DbSet<SysWebsiteSetting> SysWebsiteSetting { get; set; }
        public virtual DbSet<SysWebsiteApiSetting> SysWebsiteApiSetting { get; set; }

        #endregion

        #region 系统通知

        public virtual DbSet<SysNotification> SysNotification { get; set; }
        public virtual DbSet<SysNotificationToAccount> SysNotificationUserRecord { get; set; }
        public virtual DbSet<SysContactUs> SysContactUs { get; set; }

        #endregion

        #region 服务开通
        public virtual DbSet<SysService> SysService { get; set; }
        public virtual DbSet<SysServicePackage> SysServicePackage { get; set; }
        #endregion

        #region 微信用户
        public virtual DbSet<SysWechatUser> SysWechatUser { get; set; }
        public virtual DbSet<SysWxgzhSubscribeUser> SysWxgzhSubscribeUser { get; set; }
        public virtual DbSet<SysWxgzhReplySetting> SysWxgzhReplySetting { get; set; }
        public virtual DbSet<SysWxClient> SysWxClient { get; set; }
        public virtual DbSet<SysWxClientContact> SysWxClientContact { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 菜单权限

            modelBuilder.Entity<SysMenu>(form =>
            {
                form.ToTable("Sys_Menu");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysPermission>(form =>
            {
                form.ToTable("Sys_Permission");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysTenantPermContact>(form =>
            {
                form.ToTable("Sys_TenantPermContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysRolePermContact>(form =>
            {
                form.ToTable("Sys_RolePermContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysRoleUserContact>(form =>
            {
                form.ToTable("Sys_RoleUserContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysUserPermContact>(form =>
            {
                form.ToTable("Sys_UserPermContact");
                form.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 系统用户

            modelBuilder.Entity<SysTenant>(entity =>
            {
                entity.ToTable("Sys_Tenant");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("Sys_User");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysClient>(entity =>
            {
                entity.ToTable("Sys_Client");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.ToTable("Sys_Role");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 公共数据

            modelBuilder.Entity<SysArea>(entity =>
            {
                entity.ToTable("Sys_Area");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 网站设置

            modelBuilder.Entity<SysWebsiteSetting>(entity =>
            {
                entity.ToTable("Sys_WebsiteSetting");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysWebsiteApiSetting>(entity =>
            {
                entity.ToTable("Sys_WebsiteApiSetting");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

            });

            #endregion

            #region 系统通知

            modelBuilder.Entity<SysNotification>(entity =>
            {
                entity.ToTable("Sys_Notification");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysNotificationToAccount>(entity =>
            {
                entity.ToTable("Sys_NotificationToAccount");

                entity.HasKey(e => new { e.MessageId, e.TenantId, e.UserId });
            });

            modelBuilder.Entity<SysContactUs>(entity =>
            {
                entity.ToTable("Sys_ContactUs");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion

            #region 套餐服务

            modelBuilder.Entity<SysService>(entity =>
            {
                entity.ToTable("Sys_BaseService");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysServicePackage>(entity =>
            {
                entity.ToTable("Sys_ServicePackage");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            #endregion

            #region 微信用户

            modelBuilder.Entity<SysWechatUser>(entity =>
            {
                entity.ToTable("Sys_WechatUser");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysWxgzhSubscribeUser>(entity =>
            {
                entity.ToTable("Sys_WxgzhSubscribeUser");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysWxgzhReplySetting>(entity =>
            {
                entity.ToTable("Sys_WxgzhReplySetting");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysWxClient>(entity =>
            {
                entity.ToTable("Sys_WxClient");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysWxClientContact>(entity =>
            {
                entity.ToTable("Sys_WxClientContact");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion
        }
    }
}
