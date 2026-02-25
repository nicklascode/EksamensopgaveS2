using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuth authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto req)
        {
            var user = await authService.RegisterAsync(req);
            if (user == null)
                return BadRequest("User already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto reg)
        {
            var user = await authService.LoginAsync(reg);
            if (user == null)
                return Unauthorized("User not found!");

            return Ok(user);
        }
    }
}
