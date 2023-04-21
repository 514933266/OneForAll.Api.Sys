using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 应用用户
    /// </summary>
    public class SysUserService: ISysUserService
    {
        private readonly IMapper _mapper;
        private readonly ISysUserManager _userManager;
        public SysUserService(
            IMapper mapper,
            ISysUserManager userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        public async Task<PageList<SysUserDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _userManager.GetPageAsync(pageIndex, pageSize, key);

            var items = _mapper.Map<IEnumerable<SysUserAggr>, IEnumerable<SysUserDto>>(data.Items);
            return new PageList<SysUserDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysUserForm entity)
        {
            return await _userManager.AddAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysUserForm entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _userManager.DeleteAsync(ids);
        }
    }
}
