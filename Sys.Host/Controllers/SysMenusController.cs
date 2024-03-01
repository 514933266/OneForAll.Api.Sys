using System;
using System.Collections.Generic;
using OneForAll.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Host.Models;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using Sys.Public.Models;
using Sys.Host.Filters;
using OneForAll.Core.OAuth;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysMenusController : BaseController
    {
        private readonly ISysMenuService _menuService;
        public SysMenusController(ISysMenuService menuService)
        {
            _menuService = menuService;
        }

        #region 菜单

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key"></param>
        /// <returns>菜单树</returns>
        [HttpGet]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<IEnumerable<SysMenuTreeDto>> GetListAsync(
            [FromQuery] bool hasPerms = false,
            [FromQuery] Guid? parentId = null,
            [FromQuery] string key = "")
        {
            if (parentId == null)
                parentId = Guid.Empty;
            return await _menuService.GetListAsync(hasPerms, parentId.Value, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">菜单表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.Add)]
        public async Task<BaseMessage> AddAsync([FromBody] SysMenuForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _menuService.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级菜单");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 克隆子级
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="mids">子级id</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Children")]
        [CheckPermission(Action = ConstPermission.Add)]
        public async Task<BaseMessage> CopyAsync(Guid id, [FromBody] IEnumerable<Guid> mids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _menuService.CopyAsync(id, mids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("克隆成功");
                case BaseErrType.DataNotFound: return msg.Fail("目标菜单不存在");
                case BaseErrType.DataEmpty: return msg.Fail("找不到要克隆的子级菜单");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">菜单表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission(Action = ConstPermission.Update)]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysMenuForm entity)
        {
            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _menuService.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级菜单");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">菜单表单</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/IsEnabled")]
        [CheckPermission(Action = ConstPermission.Update)]
        public async Task<BaseMessage> UpdateAsync(Guid id, [FromQuery] bool isEnable)
        {
            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _menuService.EnableAsync(id, isEnable);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
                default: return msg.Fail("操作失败");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id">菜单表单</param>
        /// <param name="sortNumber">是否启用</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/SortNumber")]
        [CheckPermission(Action = ConstPermission.Update)]
        public async Task<BaseMessage> UpdateAsync(Guid id, [FromQuery] int sortNumber)
        {
            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _menuService.SortAsync(id, sortNumber);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("操作成功");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
                default: return msg.Fail("操作失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _menuService.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataExist: return msg.Fail("当前菜单存在子级");
                case BaseErrType.NotAllow: return msg.Fail("禁止删除系统默认菜单");
                default: return msg.Fail("删除失败");
            }
        }

        #endregion

        #region 权限

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        [Route("{id}/Permissions")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<IEnumerable<SysMenuPermissionDto>> GetListPermissionAsync(Guid id)
        {
            return await _menuService.GetListPermissionAsync(id);
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="form">权限表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Permissions")]
        [CheckPermission(Action = ConstPermission.Add)]
        public async Task<BaseMessage> AddPermissionAsync(Guid id, [FromBody] IEnumerable<SysMenuPermissionForm> form)
        {
            var msg = new BaseMessage();
            var errType = await _menuService.AddPermissionAsync(id, form);

            switch (errType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("权限代码或名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("菜单不存在");
                case BaseErrType.DataEmpty: return msg.Fail("请选择要克隆的权限");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="form">权限表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [Route("{id}/Permissions")]
        [CheckPermission(Action = ConstPermission.Update)]
        public async Task<BaseMessage> UpdatePermissionAsync(Guid id, [FromBody] SysMenuPermissionForm form)
        {
            var msg = new BaseMessage();
            var errType = await _menuService.UpdatePermissionAsync(id, form);

            switch (errType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("权限代码或名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="id">菜单id</param>
        /// <param name="permIds">权限id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/Permissions/Batch/IsDeleted")]
        [CheckPermission(Action = ConstPermission.Delete)]
        public async Task<BaseMessage> DeletePermissionAsync(Guid id, [FromBody] IEnumerable<Guid> permIds)
        {
            var msg = new BaseMessage();
            var errType = await _menuService.DeletePermissionsAsync(id, permIds);

            switch (errType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                default: return msg.Fail("删除失败");
            }
        }
        #endregion
    }
}