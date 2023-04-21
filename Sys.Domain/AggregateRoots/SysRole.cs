using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sys.Domain.AggregateRoots
{
    /// <summary>
    /// 基础表：角色
    /// </summary>
    public partial class SysRole : AggregateRoot<Guid>
    {
        public SysRole()
        {
            SysRolePermContacts = new HashSet<SysRolePermContact>();
            SysRoleUserContacts = new HashSet<SysRoleUserContact>();
        }
        /// <summary>
        /// 租户id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        public virtual SysTenant SysTenant { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Remark { get; set; } = "";

        public virtual ICollection<SysRolePermContact> SysRolePermContacts { get; set; }
        public virtual ICollection<SysRoleUserContact> SysRoleUserContacts { get; set; }

        #region 权限

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="permissions">权限</param>
        public void AddPermission(IEnumerable<SysPermission> permissions)
        {
            permissions.ForEach(perm =>
            {
                var item = SysRolePermContacts.FirstOrDefault(w => w.SysPermissionId == perm.Id);
                if (item == null)
                {
                    SysRolePermContacts.Add(new SysRolePermContact()
                    {
                        Id = Guid.NewGuid(),
                        SysRoleId = Id,
                        SysPermissionId = perm.Id
                    });
                }
            });
        }

        #endregion

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="users">用户</param>
        public void AddMember(IEnumerable<SysUser> users)
        {
            users.ForEach(e =>
            {
                var item = SysRoleUserContacts.FirstOrDefault(w => w.SysUserId.Equals(e.Id));
                if (item == null)
                {
                    SysRoleUserContacts.Add(new SysRoleUserContact()
                    {
                        Id = Guid.NewGuid(),
                        SysRoleId = Id,
                        SysUserId = e.Id
                    });
                }
            });
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        public void RemoveMember(Guid userId)
        {
            var item = SysRoleUserContacts.FirstOrDefault(w => w.SysUserId == userId);
            if (item != null)
            {
                SysRoleUserContacts.Remove(item);
            }
        }

        /// <summary>
        /// 移除用户
        /// </summary>
        /// <param name="userIds">用户id</param>
        public void RemoveMember(IEnumerable<Guid> userIds)
        {
            userIds.ForEach(userId =>
            {
                RemoveMember(userId);
            });
        }
    }
}
