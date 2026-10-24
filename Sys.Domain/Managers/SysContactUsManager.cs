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
    /// 联系我们（命令：增删）
    /// </summary>
    public class SysContactUsManager : SysDomainManager, ISysContactUsManager
    {
        private readonly IMapper _mapper;
        private readonly ISysContactUsRepository _repository;
        public SysContactUsManager(
            IMapper mapper,
            ICurrentUser currentUser,
            ISysContactUsRepository repository) : base(currentUser)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysContactUsForm form)
        {
            var data = _mapper.Map<SysContactUsForm, SysContactUs>(form);
            return await ResultAsync(() => _repository.AddAsync(data));
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
