using Sys.Domain.Entities;
using Sys.Domain.Interfaces;
using System.Threading.Tasks;

namespace Sys.Domain.Querys
{
    /// <summary>
    /// 微信登录用户查询服务（只读，返回实体，不含 DTO）
    /// </summary>
    public class SysWechatUserQueryService : ISysWechatUserQueryService
    {
        private readonly ISysWechatUserRepository _repository;

        public SysWechatUserQueryService(ISysWechatUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 按手机号获取微信登录用户
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>微信登录用户</returns>
        public async Task<SysWechatUser> GetByMobileAsync(string mobile)
        {
            return await _repository.GetByMobileAsync(mobile);
        }

        /// <summary>
        /// 按UnionId获取微信登录用户
        /// </summary>
        /// <param name="unionId">微信unionId</param>
        /// <returns>微信登录用户</returns>
        public async Task<SysWechatUser> GetByUnionIdAsync(string unionId)
        {
            return await _repository.GetByUnionIdAsync(unionId);
        }
    }
}
