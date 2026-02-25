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
            if (!ValidateUser(req))
                return BadRequest("Incorrect formating");

            var user = await authService.RegisterAsync(req);
            if (user == null)
                return BadRequest("User with the same name already exists!");

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

        private bool ValidateUser(UserDto req)
        {
            if (string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.Email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(req.Email);
                return addr.Address == req.Email;
            }
            catch
            {
                return false;
            }
        }
    }
}
