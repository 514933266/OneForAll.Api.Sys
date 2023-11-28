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
    /// 联系我们
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysContactUsController : BaseController
    {
        private readonly ISysContactUsService _permService;

        public SysContactUsController(ISysContactUsService permService)
        {
            _permService = permService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>权限列表</returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysContactUsDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key)
        {
            return await _permService.GetPageAsync(pageIndex, pageSize, key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _permService.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("请先选择要删除的数据");
                default: return msg.Fail("删除失败");
            }
        }
    }
}