﻿using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public interface ISysWxClientSettingService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWxClientSettingDto>> GetListAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysWxClientSettingForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysWxClientSettingForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);
    }
}
