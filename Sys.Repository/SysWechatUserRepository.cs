using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    public class SysWechatUserRepository : Repository<SysWechatUser>, ISysWechatUserRepository
    {
        public SysWechatUserRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 按手机号查询实体
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns>微信用户</returns>
        public async Task<SysWechatUser> GetByMobileAsync(string mobile)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.Mobile == mobile);
        }

        /// <summary>
        /// 按UnionId查询实体
        /// </summary>
        /// <param name="unionId">UnionId</param>
        /// <returns>微信用户</returns>
        public async Task<SysWechatUser> GetByUnionIdAsync(string unionId)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.UnionId == unionId);
        }

        /// <summary>
        /// 按OpenId查询实体
        /// </summary>
        /// <param name="openId">OpenId</param>
        /// <returns>微信用户</returns>
        public async Task<SysWechatUser> GetByOpenIdAsync(string openId)
        {
            return await DbSet.FirstOrDefaultAsync(w => w.OpenId == openId);
        }

        /// <summary>
        /// 按UnionId集合查询列表
        /// </summary>
        /// <param name="unionIds">UnionId集合</param>
        /// <returns>微信用户列表</returns>
        public async Task<IEnumerable<SysWechatUser>> GetListByUnionIdsAsync(List<string> unionIds)
        {
            return await DbSet.Where(w => unionIds.Contains(w.UnionId)).ToListAsync();
        }
    }
}
