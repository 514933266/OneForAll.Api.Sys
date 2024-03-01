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
using Sys.Host.Filters;
using OneForAll.Core.OAuth;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysUsersController : BaseController
    {
        private readonly ISysUserService _service;
        public SysUsersController(ISysUserService userService)
        {
            _service = userService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.EnterView)]
        public async Task<PageList<SysUserDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key)
        {
            return await _service.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="ids">关键字</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        public async Task<IEnumerable<SysUserDto>> GetListAsync([FromQuery] IEnumerable<Guid> ids)
        {
            return await _service.GetListAsync(ids);
        }

        /// <summary>
        /// 根据电话获取登录用户
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="tenantId">机构id</param>
        /// <returns>用户列表</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("{mobile}")]
        public async Task<SysUserDto> GetByMobileAsync(string mobile, [FromQuery] Guid tenantId)
        {
            return await _service.GetByMobileAsync(tenantId, mobile);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [CheckPermission]
        public async Task<BaseMessage> AddAsync([FromBody] SysUserForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("用户名已被使用");
                case BaseErrType.DataError: return msg.Fail("租户不存在");
                case BaseErrType.NotAllow: return msg.Fail("该租户已经存在默认账户");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        [CheckPermission]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysUserUpdateForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("用户名已被使用");
                case BaseErrType.NotAllow: return msg.Fail("已存在其他默认账号");
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
    }
}
