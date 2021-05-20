using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.IdentityEndpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IloggerService _logger;
        private readonly IConfiguration _config;
        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IloggerService logger, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
        }
        /// <summary>
        /// Login User Endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            try
            {
            _logger.LogInfo($"Login Attempt from User {userDTO.UserName}");
            var result = await _signInManager.PasswordSignInAsync(userDTO.UserName, userDTO.Password, false, false);

            if(result.Succeeded)
            {
                _logger.LogInfo($"User {userDTO.UserName} Authenticated");
                var user = await _userManager.FindByNameAsync(userDTO.UserName);
                var token = await GenerateJWT(user);
                return Ok(new { token = token });
            }
            else
            {
                _logger.LogInfo($"User {userDTO.UserName} Not Authorized");
                return Unauthorized(userDTO.UserName);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody]  newUser)
        //{
        //    try
        //    {
                
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private async Task<string> GenerateJWT(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id),

            };
            
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], claims, null, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
