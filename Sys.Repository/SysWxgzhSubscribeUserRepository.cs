using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
