using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Security;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Upload;
using System.IO;
using OneForAll.File;
using OneForAll.Core.Extension;

namespace Sys.Domain
{
    /// <summary>
    /// 网站设置
    /// </summary>
    public class SysWebsiteSettingManager : SysBaseManager, ISysWebsiteSettingManager
    {
        // 文件存储路径
        private readonly string UPLOAD_PATH = "/upload/websetting/{0}/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "/resources/websetting/{0}/";

        private readonly IUploader _uploader;
        private readonly ISysWebsiteSettingRepository _repository;

        public SysWebsiteSettingManager(
            IMapper mapper,
            IUploader uploader,
            IHttpContextAccessor httpContextAccessor,
            ISysWebsiteSettingRepository repository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页</returns>
        public async Task<PageList<SysWebsiteSetting>> GetPageAsync(int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<SysWebsiteSetting> GetAsync(Guid id)
        {
            return await _repository.FindAsync(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysWebsiteSettingForm entity)
        {
            var data = await _repository.GetByHostAsync(entity.Host);
            if (data != null) return BaseErrType.DataExist;

            data = _mapper.Map<SysWebsiteSettingForm, SysWebsiteSetting>(entity);
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">用户</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysWebsiteSettingForm entity)
        {
            var data = await _repository.GetByHostAsync(entity.Host);
            if (data != null && data.Id != entity.Id) return BaseErrType.DataExist;

            data = await _repository.FindAsync(entity.Id);
            if (data == null) return BaseErrType.DataNotFound;

            _mapper.Map(entity, data);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsTrackingAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataNotFound;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
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
            var result = new UploadResult() { Original = filename, Title = filename };
            if (id == Guid.Empty) id = Guid.NewGuid();

            if (new ValidateImageType().Validate(filename, file))
            {
                // 将文件名md5，避免中文或特殊符号影响
                var extension = Path.GetExtension(filename);
                var name = Path.GetFileNameWithoutExtension(filename);
                filename = name.ToMd5().Append(extension);
                result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(id), filename, 1 * 1024 * 1024 * 1024) as UploadResult;
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = VIRTUAL_PATH.Fmt(id).Append(filename);
                    var data = await _repository.FindAsync(id);
                    if (data != null)
                    {
                        data.LoginBackgroundUrl = result.Url;
                        await _repository.UpdateAsync(data);
                    }
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }
    }
}
