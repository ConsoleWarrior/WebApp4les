using Microsoft.AspNetCore.Mvc;
using WebAppForDocker.Abstraction;
using WebAppForDocker.Dtos;
using WebAppForDocker.Models;

namespace WebAppForDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        [HttpPost]
        public ActionResult<int> AddUser(UserDto user)
        {
            try
            {
                return Ok(_repository.AddUser(user));
            }
            catch (Exception ex) 
            {
                return StatusCode(409);
            }
        }

        [HttpGet]
        public ActionResult<RoleId> CheckUser(LoginDto loginDto)
        {
            try
            {
                return Ok(_repository.CheckUser(loginDto));
            }
            catch (Exception ex)
            {
                return StatusCode(409);
            }
        }
    }
}
