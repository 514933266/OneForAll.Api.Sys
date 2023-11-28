using System;
using System.Collections.Generic;
using OneForAll.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sys.Host.Models;
using Sys.Domain.Models;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Public.Models;
using Sys.Host.Filters;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 地区
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    public class SysAreasController : BaseController
    {
        private readonly ISysAreaService _areaService;

        public SysAreasController(ISysAreaService areaService)
        {
            _areaService = areaService;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<PageList<SysAreaDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] string key, [FromQuery] int parentId = -1)
        {
            return await _areaService.GetPageAsync(pageIndex, pageSize, key, parentId);
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        [HttpGet]
        [Route("Provinces")]
        public async Task<IEnumerable<SysAreaDto>> GetProvinceAsync()
        {
            return await _areaService.GetListProvinceAsync();
        }

        /// <summary>
        /// 获取子级地区
        /// </summary>
        [HttpGet]
        [Route("{id}/Children")]
        public async Task<IEnumerable<SysAreaDto>> GetChildrenAsync(int id)
        {
            return await _areaService.GetChildrenAsync(id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<BaseMessage> AddAsync([FromBody] SysAreaForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _areaService.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("代码已存在");
                case BaseErrType.DataNotFound: return msg.Fail("上级不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<BaseMessage> UpdateAsync([FromBody] SysAreaForm entity)
        {

            var msg = new BaseMessage();
            msg.ErrType = await _areaService.UpdateAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("代码已存在");
                case BaseErrType.DataNotFound: return msg.Fail("上级不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">地区id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        [CheckPermission(Action = ConstPermission.VIEW)]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<int> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _areaService.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Success("请先选择要删除的地区");
                default: return msg.Fail("删除失败");
            }
        }
    }
}
