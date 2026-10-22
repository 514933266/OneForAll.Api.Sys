using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 地区（命令：增删改）
    /// </summary>
    public class SysAreaManager : BaseManager, ISysAreaManager
    {
        private readonly IMapper _mapper;
        private readonly ISysAreaRepository _areaRepository;
        public SysAreaManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysAreaRepository areaRepository) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _areaRepository = areaRepository;
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
            _mapper.Map(entity, data);

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
