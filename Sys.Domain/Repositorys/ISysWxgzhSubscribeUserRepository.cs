using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信公众号订阅用户
    /// </summary>
    public interface ISysWxgzhSubscribeUserRepository : IEFCoreRepository<SysWxgzhSubscribeUser>
    {
    }
}
