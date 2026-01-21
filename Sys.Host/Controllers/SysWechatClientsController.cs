using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using Sys.Application.Dtos;
using System.Threading.Tasks;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using System.Collections.Generic;
using OneForAll.Core.OAuth;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 微信公众号
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class SysWechatClientsController : BaseController
    {
        private readonly ISysWechatClientService _service;

        public SysWechatClientsController(ISysWechatClientService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
        public async Task<IEnumerable<SysWechatClientDto>> GetPageAsync()
        {
            return await _service.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody] SysWechatClientgForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("AppId已存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysWechatClientgForm entity)
        {

            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _service.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("AppId已存在");
                case BaseErrType.DataNotFound: return msg.Fail("数据不存在");
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
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("请先选择要删除的数据");
                default: return msg.Fail("删除失败");
            }
        }
    }
}