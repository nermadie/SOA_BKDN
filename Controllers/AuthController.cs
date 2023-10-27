using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Databases.Entities;
using DatingApp.API.DTOs;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public IActionResult Post([FromBody] AuthRegisterDto registerUser)
        {
            registerUser.Username = registerUser.Username.ToLower();
            if (_userService.GetUserByUsername(registerUser.Username) is not null)
            {
                return BadRequest("Username already registered");
            }
            // Add Salt
            using var hashFunc = new HMACSHA256();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUser.Password);
            var newUser = new User
            {
                Username = registerUser.Username,
                Email = registerUser.Username,
                PasswordHash = hashFunc.ComputeHash(passwordBytes),
                PasswordSalt = hashFunc.Key
            };
            _userService.CreateUser(newUser);
            return Ok(_tokenService.GenerateToken(newUser));
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRegisterDto loginUser)
        {
            loginUser.Username = loginUser.Username.ToLower();
            var existedUser = _userService.GetUserByUsername(loginUser.Username);
            if (existedUser is null)
            {
                return Unauthorized();
            }
            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordSalt = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordSalt.Length; i++)
            {
                if (passwordSalt[i] != existedUser.PasswordHash[i])
                {
                    return Unauthorized();
                }
            }
            return Ok(_tokenService.GenerateToken(existedUser));
        }
    }
}