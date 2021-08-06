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
        private IProfileDataService _IProfileDataService;

        public ProfileController(IProfileDataService iProfileDataService)
        {
            _IProfileDataService = iProfileDataService;
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
            return await _IProfileDataService.PutProfile(user);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityUser>> PostProfile(IdentityUser user)
        {
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
