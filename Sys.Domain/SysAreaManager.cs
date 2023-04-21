using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 地区
    /// </summary>
    public class SysAreaManager : BaseManager, ISysAreaManager
    {
        private readonly IMapper _mapper;
        private readonly ISysAreaRepository _areaRepository;
        public SysAreaManager(
            IMapper mapper,
            ISysAreaRepository areaRepository)
        {
            _mapper = mapper;
            _areaRepository = areaRepository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="parentId">上级id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysArea>> GetPageAsync(int pageIndex, int pageSize, string key, int parentId)
        {
            return await _areaRepository.GetPageAsync(pageIndex, pageSize, key, parentId);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysArea>> GetListProvinceAsync()
        {
            return await _areaRepository.GetChildrenAsync(0);
        }

        /// <summary>
        /// 获取子级
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns>列表</returns>

        public async Task<IEnumerable<SysArea>> GetChildrenAsync(int parentId)
        {
            return await _areaRepository.GetChildrenAsync(parentId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysAreaForm entity)
        {
            var data = await _areaRepository.GetByCodeAsync(entity.Code);
            if (data != null) return BaseErrType.DataExist;

            data = _mapper.Map<SysAreaForm, SysArea>(entity);
            if (data.ParentId != 0)
            {
                var parent = await _areaRepository.FindAsync(entity.ParentId);
                if (parent == null) return BaseErrType.DataNotFound;
                data.Level = (byte)(parent.Level + 1);
            }
            else
            {
                data.Level = 1;
            }
            return await ResultAsync(() => _areaRepository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysAreaForm entity)
        {
            var data = await _areaRepository.GetByCodeAsync(entity.Code);
            if (data != null && data.Id != entity.Id) return BaseErrType.DataExist;

            data = await _areaRepository.FindAsync(entity.Id);
            data.MapFrom(entity);

            if (entity.ParentId != 0)
            {
                var parent = await _areaRepository.FindAsync(entity.ParentId);
                if (parent == null) return BaseErrType.DataNotFound;
                data.Level = (byte)(parent.Level + 1);
            }
            else
            {
                data.Level = 1;
            }

            return await ResultAsync(() => _areaRepository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<int> ids)
        {
            if (ids.Count() < 1) return BaseErrType.DataEmpty;
            var data = await _areaRepository.GetListAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataEmpty;

            return await ResultAsync(() => _areaRepository.DeleteRangeAsync(data));
        }
    }
}
