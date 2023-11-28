using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using System.Collections.Generic;
using Sys.Public.Models;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysPermissionsController : BaseController
    {
        private readonly ISysPermissionService _permService;

        public SysPermissionsController(ISysPermissionService permService)
        {
            _permService = permService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysPermissionDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key, [FromQuery] Guid menuId)
        {
            return await _permService.GetPageAsync(pageIndex, pageSize, key, menuId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        [CheckPermission]
        public async Task<BaseMessage> AddAsync([FromBody] SysPermissionForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _permService.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("权限代码已存在");
                case BaseErrType.DataNotFound: return msg.Fail("菜单不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        [CheckPermission]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysPermissionForm entity)
        {

            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _permService.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("权限代码已存在");
                case BaseErrType.DataNotFound: return msg.Fail("菜单不存在");
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
            msg.ErrType = await _permService.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("请先选择要删除的权限");
                default: return msg.Fail("删除失败");
            }
        }
    }
}