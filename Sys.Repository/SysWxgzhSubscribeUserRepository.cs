using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;
using OneForAll.Core.Extension;

namespace Sys.Repository
{
    /// <summary>
    /// 微信公众号订阅用户
    /// </summary>
    public class SysWxgzhSubscribeUserRepository : Repository<SysWxgzhSubscribeUser>, ISysWxgzhSubscribeUserRepository
    {
        public SysWxgzhSubscribeUserRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWxgzhSubscribeUserAggr>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<SysUser>(w => true);
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.UserName == key || w.Name.Contains(key));

            var total = await DbSet.CountAsync();
            var userDbSet = Context.Set<SysUser>().Where(predicate);
            var query = (from suser in DbSet
                         join user in userDbSet on suser.SysUserId equals user.Id into leftJoinUser
                         from ltUser in leftJoinUser.DefaultIfEmpty()
                         select new SysWxgzhSubscribeUserAggr()
                         {
                             Id = suser.Id,
                             AppId = suser.AppId,
                             SysUserId = suser.SysUserId,
                             OpenId = suser.OpenId,
                             UnionId = suser.UnionId,
                             SubscribeType = suser.SubscribeType,
                             IsUnSubscribed = suser.IsUnSubscribed,
                             CreateTime = suser.CreateTime,
                             SysUser = ltUser
                         });

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<SysWxgzhSubscribeUserAggr>(total, pageSize, pageIndex, data);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWxgzhSubscribeUserAggr>> GetListByUserAsync(IEnumerable<Guid> userIds)
        {
            var userDbSet = Context.Set<SysUser>().Where(w => userIds.Contains(w.Id));
            var query = (from suser in DbSet
                         join user in userDbSet on suser.SysUserId equals user.Id into leftJoinUser
                         from ltUser in leftJoinUser.DefaultIfEmpty()
                         select new SysWxgzhSubscribeUserAggr()
                         {
                             Id = suser.Id,
                             AppId = suser.AppId,
                             SysUserId = suser.SysUserId,
                             OpenId = suser.OpenId,
                             UnionId = suser.UnionId,
                             SubscribeType = suser.SubscribeType,
                             IsUnSubscribed = suser.IsUnSubscribed,
                             CreateTime = suser.CreateTime,
                             SysUser = ltUser
                         });

            return await query.ToListAsync();
        }
    }
}
