using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppForDocker.Controllers
{
    public class TestAccessControler : Controller
    {
        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminAction()
        {
            return Ok("Admin working");
        }

        [HttpGet]
        [Route("user")]
        [Authorize(Roles = "User")]
        public IActionResult UserAction()
        {
            return Ok("User working");
        }
    }
}
