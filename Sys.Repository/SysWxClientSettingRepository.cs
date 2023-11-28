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
    /// 微信客户端
    /// </summary>
    public class SysWxClientSettingRepository : Repository<SysWxClientSetting>, ISysWxClientSettingRepository
    {
        public SysWxClientSettingRepository(DbContext context)
            : base(context)
        {

        }
    }
}
