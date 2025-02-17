using Microsoft.AspNetCore.Mvc;

namespace DummbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {

        }

        // GET: api/Test
        [HttpGet]
        public ActionResult TestConnection() => new OkObjectResult("Test successful!");
    }
}
