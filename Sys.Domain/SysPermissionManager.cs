using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.POIFS.FileSystem;
using Sys.Domain.Aggregates;
using OneForAll.Core.Extension;

namespace Sys.Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    public class SysPermissionManager : BaseManager, ISysPermissionManager
    {
        private readonly IMapper _mapper;
        private readonly ISysPermissionRepository _repository;
        public SysPermissionManager(
            IMapper mapper,
            ISysPermissionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysPermissionAggr>> GetPageAsync(int pageIndex, int pageSize, string key, Guid menuId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageWithMenuAsync(pageIndex, pageSize, key, menuId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysPermissionForm form)
        {
            var exists = await _repository.GetListByMenuAsync(form.MenuId);
            if (exists.Any(w => w.Code == form.Code))
                return BaseErrType.DataExist;

            var data = _mapper.Map<SysPermissionForm, SysPermission>(form);
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="forms">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(IEnumerable<SysPermissionForm> forms)
        {
            var data = _mapper.Map<IEnumerable<SysPermissionForm>, IEnumerable<SysPermission>>(forms);
            if (!data.Any())
                return BaseErrType.DataEmpty;
            data.ForEach(e => e.Id = Guid.Empty);// 清空Id，自动生成
            if (data.Count() == 1)
            {
                var form = data.First();
                var exists = await _repository.GetListByMenuAsync(form.SysMenuId);
                if (exists.Any(w => w.Code == form.Code))
                    return BaseErrType.DataExist;

                return await ResultAsync(() => _repository.AddAsync(form));
            }
            else
            {
                return await ResultAsync(() => _repository.AddRangeAsync(data));
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysPermissionForm form)
        {
            if (form.MenuId != Guid.Empty)
            {
                var exists = await _repository.GetListByMenuAsync(form.MenuId);
                if (exists.Any(w => w.Code == form.Code && w.Id != form.Id))
                    return BaseErrType.DataExist;
                var data = exists.FirstOrDefault(w => w.Id == form.Id);
                if (data == null)
                    return BaseErrType.DataNotFound;
                _mapper.Map(form, data);
                return await ResultAsync(() => _repository.UpdateAsync(data));
            }
            else
            {
                var data = await _repository.FindAsync(form.Id);
                if (data == null)
                    return BaseErrType.DataNotFound;
                var exists = await _repository.GetListByMenuAsync(data.SysMenuId);
                if (exists.Any(w => w.Code == form.Code && w.Id != data.Id))
                    return BaseErrType.DataExist;

                data.Code = form.Code;
                data.SortCode = form.SortCode;
                data.Remark = form.Remark;
                return await ResultAsync(() => _repository.UpdateAsync(data));
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (!ids.Any())
                return BaseErrType.DataEmpty;
            var data = await _repository.GetListAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}
