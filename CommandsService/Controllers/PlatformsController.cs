using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        public ActionResult TestInBoundConnection()
        {
            Console.WriteLine("--> InBound Post # command service");
            return Ok("Inbound test of from Platforms controller");
        }
    }
}
