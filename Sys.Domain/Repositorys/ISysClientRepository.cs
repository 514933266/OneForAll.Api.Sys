using OneForAll.EFCore;
using Sys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 客户端
    /// </summary>
    public interface ISysClientRepository : IEFCoreRepository<SysClient>
    {
        /// <summary>
        /// 是否存在指定ClientId
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsByClientIdAsync(string clientId);

        /// <summary>
        /// 是否存在指定ClientId（排除自身）
        /// </summary>
        /// <param name="clientId">客户端id</param>
        /// <param name="excludeId">排除的实体id</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsByClientIdAsync(string clientId, Guid excludeId);

        /// <summary>
        /// 按id集合查询列表
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysClient>> GetListByIdsAsync(IEnumerable<Guid> ids);
    }
}
