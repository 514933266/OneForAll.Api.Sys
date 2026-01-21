using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneForAll.Core;
using System.Threading.Tasks;
using Sys.Domain.Models;
using Sys.Application.Interfaces;
using OneForAll.Core.OAuth;

namespace Sys.Host.Controllers
{
    /// <summary>
    /// 联系我们
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.Ruler)]
    public class ContactUsController : BaseController
    {
        private readonly ISysContactUsService _service;

        public ContactUsController(ISysContactUsService service)
        {
            _service = service;
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody] SysContactUsForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("信息已提交，我们将会尽快安排人员联系您");
                default: return msg.Fail("提交留言失败");
            }
        }
    }
}