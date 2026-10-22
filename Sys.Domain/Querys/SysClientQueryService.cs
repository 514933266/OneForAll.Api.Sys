using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 系统客户端查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysClientQueryService : ISysClientQueryService
    {
        private readonly ISysClientRepository _repository;

        public SysClientQueryService(ISysClientRepository repository)
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
    }
}
