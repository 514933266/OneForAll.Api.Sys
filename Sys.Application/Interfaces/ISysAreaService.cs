using Sys.Application.Dtos;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    /// <summary>
    /// 系统地区应用服务
    /// </summary>
    public interface ISysAreaService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysAreaDto>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId);

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns>列表</returns>
        Task<IEnumerable<SysAreaDto>> GetListProvinceAsync();


        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysAreaDto>> GetChildrenAsync(int parentId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">地区</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(SysAreaForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">地区</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(SysAreaForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<int> ids);
    }
}
