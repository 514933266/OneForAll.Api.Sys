using AutoMapper;
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
    /// 系统客户端（命令：增删改）
    /// </summary>
    public class SysClientManager : SysDomainManager, ISysClientManager
    {
        private readonly IMapper _mapper;
        private readonly ISysClientRepository _repository;

        public SysClientManager(
            IMapper mapper,
            ICurrentUser currentUser,
            ISysClientRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysClientForm form)
        {
            var exists = await _repository.ExistsByClientIdAsync(form.ClientId);
            if (exists)
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
            var exists = await _repository.ExistsByClientIdAsync(form.ClientId, form.Id);
            if (exists)
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
            var data = await _repository.GetListByIdsAsync(ids);
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }
    }
}
