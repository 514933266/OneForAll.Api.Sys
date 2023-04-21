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
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<SysTenant> SysTenant { get; set; }

        public virtual DbSet<SysTenantPermContact> SysTenantPermContact { get; set; }

        public virtual DbSet<SysUser> SysUser { get; set; }

        public virtual DbSet<SysUserPermContact> SysUserPermContact { get; set; }

        public virtual DbSet<SysArea> SysArea { get; set; }

        public virtual DbSet<SysWebsiteSetting> SysWebsiteSetting { get; set; }
        public virtual DbSet<SysWebsiteApiSetting> SysWebsiteApiSetting { get; set; }
        public virtual DbSet<SysNotification> SysNotification { get; set; }
        public virtual DbSet<SysNotificationToAccount> SysNotificationUserRecord { get; set; }
        public virtual DbSet<SysService> SysBaseService { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 菜单权限

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.ToTable("Sys_Menu");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysPermission>(entity =>
            {
                entity.ToTable("Sys_Permission");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

            });

            #endregion

            #region 系统用户

            modelBuilder.Entity<SysTenant>(entity =>
            {
                entity.ToTable("Sys_Tenant");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysTenantPermContact>(entity =>
            {
                entity.ToTable("Sys_TenantPermContact");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("Sys_User");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SysUserPermContact>(entity =>
            {
                entity.ToTable("Sys_UserPermContact");

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

            #endregion

            #region 套餐服务

            modelBuilder.Entity<SysService>(entity =>
            {
                entity.ToTable("Sys_BaseService");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            #endregion
        }
    }
}
