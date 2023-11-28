using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Interfaces
{
    /// <summary>
    /// 微信登录用户
    /// </summary>
    public interface ISysWechatUserRepository : IEFCoreRepository<SysWechatUser>
    {
        
    }
}
