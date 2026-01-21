using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Repository
{
    /// <summary>
    /// 微信客户端关联
    /// </summary>
    public class SysMidWechatClientRepository : Repository<SysMidWechatClient>, ISysMidWechatClientRepository
    {
        public SysMidWechatClientRepository(DbContext context)
            : base(context)
        {

        }
    }
}
