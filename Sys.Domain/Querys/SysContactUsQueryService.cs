using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 联系我们查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysContactUsQueryService : ISysContactUsQueryService
    {
        private readonly ISysContactUsRepository _repository;

        public SysContactUsQueryService(ISysContactUsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysContactUs>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }
    }
}
