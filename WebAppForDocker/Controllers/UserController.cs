using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAppForDocker.Abstraction;
using WebAppForDocker.Dtos;
using WebAppForDocker.Models;
using WebAppForDocker.RSATools;

namespace WebAppForDocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public UserController(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("AddUser")]
        public ActionResult<int> AddUser(UserDto user)
        {
            try
            {
                return Ok(_repository.AddUser(user));
            }
            catch (Exception ex) 
            {
                return StatusCode(409, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDto login)
        {
            try
            {
                var roleId = _repository.CheckUser(login);

                var user = new UserDto
                {
                    Name = login.Name,
                    Password = login.Password,
                    Role = roleId
                };

                var token = GenerateToken(user);

                return Ok(token);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }

        }

        private string GenerateToken(UserDto user)
        {
            var securityKey = new RsaSecurityKey(RSAExtensions.GetPrivateKey());

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
