using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyBGList.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }

        [Route("/error/test")]
        [HttpGet]
        public IActionResult Test()
        {
            throw new Exception("test");
        }
    }
}
