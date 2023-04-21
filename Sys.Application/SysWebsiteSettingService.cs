using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Upload;
using System.IO;
using OneForAll.File;
using OneForAll.Core.Security;
using System.Linq;
using OneForAll.Core.Extension;
using Sys.Domain.Repositorys;
using Sys.Domain.Aggregates;

namespace Sys.Application
{
    /// <summary>
    /// 应用权限
    /// </summary>
    public class SysWebsiteSettingService : ISysWebsiteSettingService
    {
        private readonly IMapper _mapper;
        private readonly ISysWebsiteSettingManager _manager;
        private readonly ISysWebsiteApiSettingManager _apiManager;

        private readonly ISysWebsiteSettingRepository _repository;

        public SysWebsiteSettingService(
            IMapper mapper,
            ISysWebsiteSettingManager manager,
            ISysWebsiteApiSettingManager apiManager,
            ISysWebsiteSettingRepository repository)
        {
            _mapper = mapper;
            _manager = manager;
            _apiManager = apiManager;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysWebsiteSettingDto>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key);
            var items = _mapper.Map<IEnumerable<SysWebsiteSetting>, IEnumerable<SysWebsiteSettingDto>>(data.Items);
            return new PageList<SysWebsiteSettingDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSettingDto> GetAsync(Guid id)
        {
            var data = await _manager.GetAsync(id);
            return _mapper.Map<SysWebsiteSetting, SysWebsiteSettingDto>(data);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSettingDto> GetAsync(string host)
        {
            var data = await _repository.GetByHostWithContactAsync(host);
            var result = _mapper.Map<SysWebsiteSettingAggr, SysWebsiteSettingDto>(data);
            if (result.Host.IsNullOrEmpty())
            {
                result.Name = "未注册授权域名";
            }
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWebsiteSettingForm entity)
        {
            return await _manager.AddAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWebsiteSettingForm entity)
        {
            return await _manager.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="filename">文件名称</param>
        /// <param name="file">数据流</param>
        /// <returns></returns>
        public async Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file)
        {
            return await _manager.UploadImageAsync(id, filename, file);
        }

        #region Api授权设置

        /// <summary>
        /// 获取api设置列表
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<SysWebsiteApiSettingDto>> GetListApiAsync(Guid id)
        {
            var data = await _apiManager.GetListAsync(id);
            return _mapper.Map<IEnumerable<SysWebsiteApiSetting>, IEnumerable<SysWebsiteApiSettingDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddApiAsync(Guid id, SysWebsiteApiSettingForm entity)
        {
            return await _apiManager.AddAsync(id, entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateApiAsync(Guid id, SysWebsiteApiSettingForm entity)
        {
            return await _apiManager.UpdateAsync(id, entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="ids">api设置id</param>
        /// <returns>消息</returns>
        public async Task<BaseErrType> DeleteApiAsync(Guid id, IEnumerable<Guid> ids)
        {
            return await _apiManager.DeleteAsync(id, ids);
        }

        #endregion

    }
}
