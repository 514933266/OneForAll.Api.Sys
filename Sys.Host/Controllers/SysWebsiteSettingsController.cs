using System;
using System.Threading.Tasks;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.Models;
using Sys.Host.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using System.Collections.Generic;
using Sys.Public.Models;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Upload;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 网站设置
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysWebsiteSettingsController : BaseController
    {
        private readonly ISysWebsiteSettingService _service;
        public SysWebsiteSettingsController(ISysWebsiteSettingService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysWebsiteSettingDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        [HttpGet]
        [Route("{id}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<SysWebsiteSettingDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission]
        public async Task<BaseMessage> AddAsync([FromBody] SysWebsiteSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("域名已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysWebsiteSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("域名已存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">租户id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("请先选择要删除的数据");
                default: return msg.Fail("删除失败");
            }
        }

        #region Api

        /// <summary>
        /// 查询Api授权设置列表
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        [HttpGet]
        [Route("{id}/Apis")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<IEnumerable<SysWebsiteApiSettingDto>> GetListApiAsync(Guid id)
        {
            return await _service.GetListApiAsync(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Apis")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> AddApiAsync(Guid id, [FromBody] SysWebsiteApiSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddApiAsync(id, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加Api设置成功");
                case BaseErrType.DataExist: return msg.Fail("域名已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="entity">表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [Route("{id}/Apis")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> UpdateApiAsync(Guid id, [FromBody] SysWebsiteApiSettingForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateApiAsync(id, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改Api设置成功");
                case BaseErrType.DataExist: return msg.Fail("域名已存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="ids">租户id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("{id}/Apis/Batch/IsDeleted")]
        [CheckPermission(Action = ConstPermission.DELETE)]
        public async Task<BaseMessage> DeleteApiAsync(Guid id, [FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteApiAsync(id, ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("请先选择要删除的数据");
                default: return msg.Fail("删除失败");
            }
        }

        #endregion

        #region 背景

        /// <summary>
        /// 上传封面
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="form">文件表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/BackgroundUrl")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> UploadImageAsync(Guid id, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                if (id == Guid.Empty) id = Guid.NewGuid();
                var file = form.Files[0];
                var callbacks = await _service.UploadImageAsync(id, file.FileName, file.OpenReadStream());

                msg.Data = new { Id = id, Result = callbacks };

                switch (callbacks.State)
                {
                    case UploadEnum.Success: return msg.Success("上传成功");
                    case UploadEnum.Overflow: return msg.Fail("文件超出限制大小1MB");
                    case UploadEnum.Error: return msg.Fail("上传过程中发生未知错误");
                }
            }
            return msg.Fail("上传失败，请选择文件");
        }

        #endregion
    }
}
