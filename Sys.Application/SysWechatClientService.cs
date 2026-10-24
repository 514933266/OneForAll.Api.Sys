using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Entities;
using Sys.Domain.Aggregates;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application
{
    /// <summary>
    /// 微信客户端
    /// </summary>
    public class SysWechatClientService : ISysWechatClientService
    {
        private readonly IMapper _mapper;
        private readonly ISysWechatClientManager _manager;
        private readonly ISysWechatClientQueryService _query;

        public SysWechatClientService(
            IMapper mapper,
            ISysWechatClientManager manager,
            ISysWechatClientQueryService query)
        {
            _mapper = mapper;
            _manager = manager;
            _query = query;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWechatClientDto>> GetListAsync()
        {
            var data = await _query.GetListAsync();
            return _mapper.Map<IEnumerable<SysWechatClientAggr>, IEnumerable<SysWechatClientDto>>(data);
        }

        /// <summary>
        /// 获取指定客户端的AccessToken
        /// </summary>
        /// <param name="clientId">系统客户端id</param>
        /// <returns>AccessToken</returns>
        public async Task<string> GetAccessTokenAsync(string clientId)
        {
            var data = await _query.GetByClientIdAsync(clientId);
            return data == null ? "" : data.AccessToken;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWechatClientgForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWechatClientgForm form)
        {
            return await _manager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }
    }
}
