using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 系统地区应用服务
    /// </summary>
    public class SysAreaService : ISysAreaService
    {
        private readonly IMapper _mapper;
        private readonly ISysAreaManager _areaManageer;

        public SysAreaService(
            IMapper mapper,
            ISysAreaManager areaManageer)
        {
            _mapper = mapper;
            _areaManageer = areaManageer;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysAreaDto>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId)
        {
            var data= await _areaManageer.GetPageAsync(pageIndex, pageSize, key, parentId);
            var items = _mapper.Map<IEnumerable<SysArea>, IEnumerable<SysAreaDto>>(data.Items);
            return new PageList<SysAreaDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysAreaDto>> GetListProvinceAsync()
        {
            var data = await _areaManageer.GetChildrenAsync(0);
            return _mapper.Map<IEnumerable<SysArea>, IEnumerable<SysAreaDto>>(data);
        }

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysAreaDto>> GetChildrenAsync(int parentId)
        {
            var data = await _areaManageer.GetChildrenAsync(parentId);
            return _mapper.Map<IEnumerable<SysArea>, IEnumerable<SysAreaDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysAreaForm form)
        {
            return await _areaManageer.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysAreaForm form)
        {
            return await _areaManageer.UpdateAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<int> ids)
        {
            return await _areaManageer.DeleteAsync(ids);
        }
    }
}
