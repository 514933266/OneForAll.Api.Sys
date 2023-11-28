﻿using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
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
    }
}
