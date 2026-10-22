using OneForAll.Core;
using Sys.Domain.Aggregates;
using Sys.Domain.Entities;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Sys.Domain.Interfaces;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 微信公众号订阅用户查询服务（只读，返回聚合根/实体，不含 DTO）
    /// </summary>
    public class SysWechatGzhSubscriberQueryService : ISysWechatGzhSubscriberQueryService
    {
        private readonly ISysWechatGzhSubscriberRepository _repository;

        public SysWechatGzhSubscriberQueryService(ISysWechatGzhSubscriberRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">所属微信应用id</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWechatGzhSubscriberAggr>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 按用户id集合获取订阅用户
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatGzhSubscriberAggr>> GetListByUserAsync(IEnumerable<Guid> userIds)
        {
            return await _repository.GetListByUserAsync(userIds);
        }

        /// <summary>
        /// 获取指定用户在指定公众号下的有效订阅
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="appId">微信appId</param>
        /// <returns>订阅用户</returns>
        public async Task<SysWechatGzhSubscriber> GetActiveByUserAsync(Guid userId, string appId)
        {
            return await _repository.GetActiveByUserAsync(userId, appId);
        }
    }
}
