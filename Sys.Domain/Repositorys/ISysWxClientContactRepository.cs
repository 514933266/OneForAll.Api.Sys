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
    /// 微信客户端-系统客户端关联
    /// </summary>
    public interface ISysWxClientContactRepository : IEFCoreRepository<SysWxClientContact>
    {
    }
}
