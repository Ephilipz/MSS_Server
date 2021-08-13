using DataService;
using DataService.Profile;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Profile"), Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IProfileDataService _IProfileDataService;

        public ProfileController(UserManager<IdentityUser> userManager, IProfileDataService iProfileDataService,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            _IProfileDataService = iProfileDataService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<IdentityUser>>> GetProfiles()
        {
            List<IdentityUser> profiles = await _IProfileDataService.GetProfiles();
            return profiles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetProfile(string id)
        {
            IdentityUser user = await _IProfileDataService.GetProfile(id);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult<IdentityUser>> PutProfile(Client user)
        {
            IdentityUser search = await _userManager.FindByEmailAsync(user.Email);
            search.UserName = user.UserName;
            //search.BillingInformation = user.BillingInformation;
            return await _IProfileDataService.PutProfile(search);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityUser>> PostProfile(Client user)
        {
            Client search = (Client)await _userManager.FindByEmailAsync(user.Email);
            search.UserName = user.UserName;
            search.BillingInformation = user.BillingInformation;
            IdentityUser updatedProfile = await _IProfileDataService.PostProfile(search);
            if (updatedProfile == null)
            {
                return BadRequest("Unable To Update Profile");
            }
            return updatedProfile;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentityUser>> DeleteProfile(int id)
        {
            IdentityUser deletedUser = await _IProfileDataService.DeleteProfile(id);
            if (deletedUser == null)
            {
                return BadRequest("Unable To Delete Profile");
            }
            return deletedUser;
        }

        [HttpGet("GetCurrentUserWithBilling")]
        public async Task<ActionResult<IdentityUser>> GetCurrentUserWithBilling()
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            return await _IProfileDataService.GetProfileWithBilling(userId);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<IdentityUser>> GetCurrentUser()
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            return await _IProfileDataService.GetProfile(userId);
        }

        [HttpGet("IsAdmin")]
        public async Task<ActionResult<bool>> IsAdmin()
        {
            string userId = Helper.AccountHelper.getUserId(HttpContext, User);
            bool admin = await _IProfileDataService.IsAdmin(userId);
            return Ok(new { isAdmin = admin });
        }
    }
}
