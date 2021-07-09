using Entities.Authentication;
using Entities.Users;
using Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _IConfiguration;

        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration IConfiguration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _IConfiguration = IConfiguration;
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdministratorVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }
            var user = new Administrator
            {
                UserName = registerVM.FullName.Trim().Replace(" ", "."),
                Email = registerVM.Email,
                AdminId = registerVM.AdminId
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                return BadRequest(ModelState);
            }

            await _signInManager.SignInAsync(user, true);

            return Ok();
        }

        [HttpPost("RegisterClient")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }
            var user = new IdentityUser
            {
                UserName = registerVM.FullName.Trim().Replace(" ", "."),
                Email = registerVM.Email
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                return BadRequest(ModelState);
            }

            await _signInManager.SignInAsync(user, true);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("Logout")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
