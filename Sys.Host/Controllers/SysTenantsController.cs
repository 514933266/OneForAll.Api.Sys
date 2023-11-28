using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Host.Models;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using Sys.Domain.Enums;
using Sys.Public.Models;
using Autofac.Core;
using Sys.Host.Filters;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.Upload;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 租户（租户）
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysTenantsController : BaseController
    {
        private readonly ISysTenantService _service;

        public SysTenantsController(ISysTenantService service)
        {
            _service = service;
        }

        #region 租户

        /// <summary>
        /// 获取租户
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>租户</returns>
        [HttpGet]
        [Route("{id}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<SysTenantDto> GetAsync(Guid id)
        {
            return await _service.GetAsync(id);
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="isEnabled">合作状态（0未合作，1合作中）</param>
        /// <param name="startDate">注册开始时间</param>
        /// <param name="endDate">注册结束时间</param>
        /// <returns>租户列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysTenantDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            [FromQuery] string key,
            [FromQuery] SysEnabledEnum isEnabled = SysEnabledEnum.None,
            [FromQuery] DateTime? startDate = default,
            [FromQuery] DateTime? endDate = default)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key, isEnabled, startDate, endDate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        [CheckPermission]
        public async Task<BaseMessage> AddAsync([FromBody] SysTenantForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("租户名称或代码已被使用");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        [CheckPermission]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysTenantForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("租户名称或代码已被使用");
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
                case BaseErrType.NotAllow: return msg.Fail("该租户存在使用数据，无法删除");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 上传封面
        /// </summary>
        /// <param name="id">项目id</param>
        /// <param name="form">文件表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Logos")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> UploadLogoAsync(Guid id, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                if (id == Guid.Empty)
                    id = Guid.NewGuid();
                var file = form.Files[0];
                var callbacks = await _service.UploadLogoAsync(id, file.FileName, file.OpenReadStream());

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

        #region 菜单

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>菜单列表</returns>
        [HttpGet]
        [Route("{id}/Menus")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<IEnumerable<SysMenuTreeDto>> GetListMenuAsync(Guid id)
        {
            return await _service.GetListMenuAsync(id);
        }

        #endregion

        #region 权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        [Route("{id}/Permissions")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id)
        {
            return await _service.GetListPermissionAsync(id);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="entities">权限表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Permissions")]
        [CheckPermission(Action = ConstPermission.UPDATE)]
        public async Task<BaseMessage> AddPermissionAsync(Guid id, [FromBody] IEnumerable<SysMenuPermissionForm> entities)
        {
            var msg = new BaseMessage();
            var errType = await _service.AddPermissionAsync(id, entities);

            switch (errType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.DataNotFound: return msg.Fail("租户不存在");
                case BaseErrType.DataEmpty: return msg.Fail("请选择要开通的服务内容");
                default: return msg.Fail("操作失败");
            }
        }
        #endregion
    }
}