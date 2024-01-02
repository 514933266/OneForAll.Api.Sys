using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Aggregates;
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
    /// 微信客户端
    /// </summary>
    public class SysWxClientManager : SysBaseManager, ISysWxClientManager
    {
        private readonly ISysWxClientRepository _repository;
        private readonly ISysClientRepository _clientRepository;
        private readonly ISysWxClientContactRepository _contactRepository;
        public SysWxClientManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWxClientRepository repository,
            ISysClientRepository clientRepository,
            ISysWxClientContactRepository contactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWxClientAggr>> GetListAsync()
        {
            return await _repository.GetListWithClientAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWxClientgForm form)
        {
            var exists = await _repository.CountAsync(w => w.AppId == form.AppId);
            if (exists > 0)
                return BaseErrType.DataExist;
            var client = await _clientRepository.FindAsync(form.ClientId);
            if (client == null)
                return BaseErrType.DataNotMatch;

            var data = _mapper.Map<SysWxClientgForm, SysWxClient>(form);
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.AddAsync(data, tran);
                await _contactRepository.AddAsync(new SysWxClientContact()
                {
                    SysClientId = form.ClientId,
                    SysWxClientId = data.Id
                }, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWxClientgForm form)
        {
            var changed = false;
            var exists = await _repository.CountAsync(w => w.AppId == form.AppId && w.Id != form.Id);
            if (exists > 0)
                return BaseErrType.DataExist;
            var client = await _clientRepository.FindAsync(form.ClientId);
            if (client == null)
                return BaseErrType.DataNotMatch;
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;
            var contact = await _contactRepository.GetAsync(w => w.SysClientId == form.ClientId && w.SysWxClientId == form.Id);

            _mapper.Map(form, data);
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.UpdateAsync(data, tran);
                if (contact != null)
                    await _contactRepository.DeleteAsync(contact, tran);
                await _contactRepository.AddAsync(new SysWxClientContact()
                {
                    SysClientId = form.ClientId,
                    SysWxClientId = data.Id
                }, tran);
                return await ResultAsync(tran.CommitAsync);
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
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any())
                return BaseErrType.DataEmpty;

            var contacts = await _contactRepository.GetListAsync(w => ids.Contains(w.SysWxClientId));

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.DeleteRangeAsync(data, tran);
                if (contacts.Any())
                {
                    await _contactRepository.DeleteRangeAsync(contacts, tran);
                }
                return await ResultAsync(tran.CommitAsync);
            }
        }
    }
}
