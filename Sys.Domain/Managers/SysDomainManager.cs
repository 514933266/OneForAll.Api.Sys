using OneForAll.Core;
using OneForAll.Core.OAuth;
using Sys.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// Sys 模块领域服务基类：以 ICurrentUser 端口替代 IHttpContextAccessor 获取当前登录用户，
    /// 并提供与 OneForAll.Core.BaseManager 一致的影响行数结果判定逻辑
    /// </summary>
    public abstract class SysDomainManager
    {
        protected readonly ICurrentUser _currentUser;

        protected SysDomainManager(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        /// <summary>
        /// 当前登录用户信息（来自领域端口，不感知传输层）
        /// </summary>
        protected LoginUser LoginUser
        {
            get { return _currentUser?.LoginUser ?? new LoginUser(); }
        }

        /// <summary>
        /// 当前请求的站点基址（如 https://host）
        /// </summary>
        protected string BaseUrl
        {
            get { return _currentUser?.BaseUrl ?? ""; }
        }

        /// <summary>
        /// 执行一个同步操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">一个返回受影响行数的同步函数</param>
        protected BaseErrType Result(Func<int> action)
        {
            var effected = action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }

        /// <summary>
        /// 异步执行一个操作，并根据其返回的影响行数判断操作是否成功
        /// </summary>
        /// <param name="action">返回受影响行数的异步函数</param>
        protected async Task<BaseErrType> ResultAsync(Func<Task<int>> action)
        {
            var effected = await action();
            if (effected > 0)
            {
                return BaseErrType.Success;
            }
            return BaseErrType.Fail;
        }
    }
}
