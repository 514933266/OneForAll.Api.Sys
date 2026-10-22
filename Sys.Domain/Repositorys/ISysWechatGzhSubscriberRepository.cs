using OneForAll.Core;
using OneForAll.EFCore;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Repositorys
{
    /// <summary>
    /// 微信公众号订阅用户
    /// </summary>
    public interface ISysWechatGzhSubscriberRepository : IEFCoreRepository<SysWechatGzhSubscriber>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">所属微信应用id</param>
        /// <returns>分页列表</returns>
        Task<PageList<SysWechatGzhSubscriberAggr>> GetPageAsync(int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="userIds">用户id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<SysWechatGzhSubscriberAggr>> GetListByUserAsync(IEnumerable<Guid> userIds);

        /// <summary>
        /// 按OpenId查询实体
        /// </summary>
        /// <param name="openId">OpenId</param>
        /// <returns>订阅用户</returns>
        Task<SysWechatGzhSubscriber> GetByOpenIdAsync(string openId);

        /// <summary>
        /// 按appId与OpenId查询实体
        /// </summary>
        /// <param name="appId">微信appId</param>
        /// <param name="openId">OpenId</param>
        /// <returns>订阅用户</returns>
        Task<SysWechatGzhSubscriber> GetByAppIdOpenIdAsync(string appId, string openId);

        /// <summary>
        /// 按用户与appId查询有效（未取消关注）订阅用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="appId">微信appId</param>
        /// <returns>订阅用户</returns>
        Task<SysWechatGzhSubscriber> GetActiveByUserAsync(Guid userId, string appId);

        /// <summary>
        /// 查询未关联系统用户的订阅用户列表
        /// </summary>
        /// <returns>订阅用户列表</returns>
        Task<IEnumerable<SysWechatGzhSubscriber>> GetListUnboundAsync();
    }
}
