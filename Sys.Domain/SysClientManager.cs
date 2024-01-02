using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain
{
    /// <summary>
    /// 系统客户端
    /// </summary>
    public class SysClientManager : SysBaseManager, ISysClientManager
    {
        private readonly ISysClientRepository _repository;

        public SysClientManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysClientRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysClient>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysClientForm form)
        {
            var exists = await _repository.CountAsync(w => w.ClientId == form.ClientId);
            if (exists > 0)
                return BaseErrType.DataExist;

            var data = _mapper.Map<SysClientForm, SysClient>(form);
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysClientForm form)
        {
            var exists = await _repository.CountAsync(w => w.ClientId == form.ClientId && w.Id != form.Id);
            if (exists > 0)
                return BaseErrType.DataExist;
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            return await ResultAsync(_repository.SaveChangesAsync);
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
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}

