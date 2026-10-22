using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 系统通知-指定账号查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysNotificationToAccountQueryService : ISysNotificationToAccountQueryService
    {
        private readonly ISysNotificationToAccountRepository _repository;

        public SysNotificationToAccountQueryService(ISysNotificationToAccountRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 按消息id获取指定账号列表
        /// </summary>
        /// <param name="messageId">消息id</param>
        /// <returns>账号列表</returns>
        public async Task<IEnumerable<SysNotificationToAccount>> GetListByMessageAsync(Guid messageId)
        {
            return await _repository.GetListAsync(messageId);
        }
    }
}
