﻿using System;
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
using Autofac.Core;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 系统客户端
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysClientsController : BaseController
    {
        private readonly ISysClientService _service;

        public SysClientsController(ISysClientService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        [HttpGet]
    [CheckPermission(Action = ConstPermission.VIEW)]
    public async Task<IEnumerable<SysClientDto>> GetPageAsync()
    {
        return await _service.GetListAsync();
    }

    /// <summary>
    /// 添加
    /// </summary>
    [HttpPost]
    [CheckPermission]
    public async Task<BaseMessage> AddAsync([FromBody] SysClientForm entity)
    {
        var msg = new BaseMessage();
        msg.ErrType = await _service.AddAsync(entity);

        switch (msg.ErrType)
        {
            case BaseErrType.Success: return msg.Success("添加成功");
            case BaseErrType.DataExist: return msg.Fail("已存在相同AppId客户端");
            default: return msg.Fail("添加失败");
        }
    }

    /// <summary>
    /// 修改
    /// </summary>
    [HttpPut]
    [CheckPermission]
    public async Task<BaseMessage> UpdateAsync([FromBody] SysClientForm entity)
    {

        var msg = new BaseMessage() { Status = false };
        msg.ErrType = await _service.UpdateAsync(entity);

        switch (msg.ErrType)
        {
            case BaseErrType.Success: return msg.Success("修改成功");
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
    [CheckPermission]
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