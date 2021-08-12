using DataService;
using DataService.Profile;
using Entities;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingManagementSystem.Controllers
{
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private IProfileDataService _IProfileDataService;

        public ProfileController(UserManager<IdentityUser> userManager, IProfileDataService iProfileDataService,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            _IProfileDataService = iProfileDataService;
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<IdentityUser>>> GetProfiles()
        {
            List<IdentityUser> profiles = await _IProfileDataService.GetProfiles();
            return profiles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetProfile(int id)
        {
            IdentityUser user = await _IProfileDataService.GetProfile(id);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult<IdentityUser>> PutProfile(IdentityUser user)
        {
            Client search = (Client) await _userManager.FindByEmailAsync(user.Email);
            search.UserName= user.UserName;
            //search.BillingInformation = user.BillingInformation;
            return await _IProfileDataService.PutProfile(search);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityUser>> PostProfile(IdentityUser user)
        {
            Console.Write(user.ToString());
            IdentityUser updatedProfile = await _IProfileDataService.PostProfile(user);
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
    }
}
