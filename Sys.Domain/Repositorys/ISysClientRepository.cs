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
    /// 客户端
    /// </summary>
    public interface ISysClientRepository : IEFCoreRepository<SysClient>
    {
    }
}
