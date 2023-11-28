﻿using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信公众号回复
    /// </summary>
    public interface ISysWxgzhReplySettingRepository : IEFCoreRepository<SysWxgzhReplySetting>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="appId">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWxgzhReplySettingAggr>> GetPageAsync(int pageIndex, int pageSize, string appId);
    }
}
