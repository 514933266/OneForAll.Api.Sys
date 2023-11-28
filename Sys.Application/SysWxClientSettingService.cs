using AutoMapper;
using OneForAll.Core;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
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
    public class SysWxClientSettingService : ISysWxClientSettingService
    {
        private readonly IMapper _mapper;
        private readonly ISysWxClientSettingManager _manager;

        public SysWxClientSettingService(
            IMapper mapper,
            ISysWxClientSettingManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysWxClientSettingDto>> GetListAsync()
        {
            var data = await _manager.GetListAsync();
            return _mapper.Map<IEnumerable<SysWxClientSetting>, IEnumerable<SysWxClientSettingDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWxClientSettingForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWxClientSettingForm form)
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
