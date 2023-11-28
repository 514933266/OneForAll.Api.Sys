using Microsoft.AspNetCore.Mvc;

namespace Sys.Host.Controllers
{
    [Route("api/[controller]")]
    public class StartupsController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return "项目启动成功...";
        }
    }
}
