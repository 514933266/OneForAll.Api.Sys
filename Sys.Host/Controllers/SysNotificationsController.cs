using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Host.Models;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using System.Collections.Generic;
using Sys.Public.Models;
using Sys.Host.Filters;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Upload;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 系统通知
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysNotificationsController : BaseController
    {
        private readonly ISysNotificationService _service;

        public SysNotificationsController(ISysNotificationService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysNotificationDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            [FromQuery] string key,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        [CheckPermission]
        public async Task<BaseMessage> AddAsync([FromBody] SysNotificationForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        [CheckPermission]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysNotificationForm entity)
        {

            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
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
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        [HttpPost]
        [Route("{id}/Images")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> UploadImageAsync(Guid id, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                if (id.Equals(Guid.Empty)) id = Guid.NewGuid(); // 实现先传图再创建对象
                var callbacks = await _service.UploadImageAsync(id, file.FileName, file.OpenReadStream());

                msg.Data = new { Id = id, Result = callbacks };

                switch (callbacks.State)
                {
                    case UploadEnum.Success: return msg.Success("上传成功");
                    case UploadEnum.Overflow: return msg.Fail("文件超出限制大小2MB");
                    case UploadEnum.Error: return msg.Fail("上传过程中发生未知错误");
                }
            }
            return msg.Fail("上传失败，请选择文件");
        }

        /// <summary>
        /// 发布
        /// </summary>
        [HttpPatch]
        [Route("{id}/IsPublish")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> PublishAsync(Guid id)
        {
            var msg = new BaseMessage();
            var errType = await _service.PublishAsync(id);

            switch (errType)
            {
                case BaseErrType.Success: return msg.Success("发布成功");
                case BaseErrType.DataNotFound: return msg.Fail("该文章已被删除");
                case BaseErrType.NotAllow: return msg.Fail("不允许重复发布");
                default: return msg.Fail("发布失败");
            }
        }
    }
}
