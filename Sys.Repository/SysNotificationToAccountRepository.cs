using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 系统通知-指定账号
    /// </summary>
    public class SysNotificationToAccountRepository : Repository<SysNotificationToAccount>, ISysNotificationToAccountRepository
    {
        public SysNotificationToAccountRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysNotificationToAccount>> GetListAsync(Guid messageId)
        {
            return await DbSet
                .Where(w => w.MessageId == messageId)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysNotificationToAccount>> GetListAsTrackingAsync(Guid messageId)
        {
            return await DbSet
                .Where(w => w.MessageId == messageId)
                .ToListAsync();
        }
    }
}
