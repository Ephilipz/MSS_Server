﻿using Entities.Authentication;
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
            //if the email or password are not valid, returns a bad request (error code 400)
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }

            //initializes a new administrator object
            var user = new Administrator
            {
                UserName = registerVM.FullName.Trim().Replace(" ", "."),
                Email = registerVM.Email,
                AdminId = registerVM.AdminId
            };

            //create a user using the built in usermanager class
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            //if the user was not created, return a bad request with the error code and details
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                return BadRequest(ModelState);
            }

            //otherwise, sign in the new user
            await _signInManager.SignInAsync(user, true);

            return Ok();
        }

        [HttpPost("RegisterClient")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientVM registerVM)
        {
            //if the email or password are not valid, returns a bad request (error code 400)
            if (!ModelState.IsValid)
            {
                return BadRequest(registerVM);
            }

            //initializes a new client object
            var user = new IdentityUser
            {
                UserName = registerVM.FullName.Trim().Replace(" ", "."),
                Email = registerVM.Email
            };

            //create a user using the built in usermanager class
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            //if the user was not created, return a bad request with the error code and details
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError(result.Errors.First().Code, result.Errors.First().Description);
                return BadRequest(ModelState);
            }

            //otherwise, sign in the new user
            await _signInManager.SignInAsync(user, true);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            //if the entity is not valid, return a Bad Request (error code 400)
            if (!ModelState.IsValid)
            {
                return BadRequest(loginVM);
            }

            //get the user object using their email via the userManager class
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            //if the user exists and the password matches, sign in the user
            if (user != null &&
                await _userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            //otherwise, add an error to the response and return a Bad Request
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
