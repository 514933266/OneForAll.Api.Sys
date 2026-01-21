using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.Entities;
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
    public class SysWechatClientManager : BaseManager, ISysWechatClientManager
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatClientRepository _repository;
        private readonly ISysClientRepository _clientRepository;
        private readonly ISysMidWechatClientRepository _contactRepository;
        public SysWechatClientManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            ISysWechatClientRepository repository,
            ISysClientRepository clientRepository,
            ISysMidWechatClientRepository contactRepository) : base(httpContextAccessor)
        {
            _mapper = mapper;
            _repository = repository;
            _clientRepository = clientRepository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatClientAggr>> GetListAsync()
        {
            return await _repository.GetListWithClientAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWechatClientgForm form)
        {
            var exists = await _repository.CountAsync(w => w.AppId == form.AppId);
            if (exists > 0)
                return BaseErrType.DataExist;
            var client = await _clientRepository.FindAsync(form.ClientId);
            if (client == null)
                return BaseErrType.DataNotMatch;

            var data = _mapper.Map<SysWechatClientgForm, SysWechatClient>(form);
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.AddAsync(data, tran);
                await _contactRepository.AddAsync(new SysMidWechatClient()
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
        public async Task<BaseErrType> UpdateAsync(SysWechatClientgForm form)
        {
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
                await _contactRepository.AddAsync(new SysMidWechatClient()
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
