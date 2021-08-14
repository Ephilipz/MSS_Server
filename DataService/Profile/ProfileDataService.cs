using DataAccess.Profile;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Profile
{
    public class ProfileDataService : IProfileDataService
    {
        private IProfileDataAccess _IProfileDataAccess;
        public ProfileDataService(IProfileDataAccess iProfileDataAccess)
        {
            _IProfileDataAccess = iProfileDataAccess;
        }
        public async Task<IdentityUser> DeleteProfile(int id)
        {
            return await _IProfileDataAccess.DeleteProfile(id);
        }

        public async Task<IdentityUser> GetProfile(string id)
        {
            return await _IProfileDataAccess.GetProfile(id);
        }

        public async Task<Client> GetProfileWithBilling(string id)
        {
            return await _IProfileDataAccess.GetProfileWithBilling(id);
        }

        public async Task<List<Client>> GetProfiles()
        {
            return await _IProfileDataAccess.GetProfiles();
        }

        public async Task<IdentityUser> PostProfile(IdentityUser user)
        {
            user.UserName = user.UserName.Trim().Replace(" ", ".");
            return await _IProfileDataAccess.PostProfile(user);
        }

        public async Task<IdentityUser> PutProfile(IdentityUser user)
        {
            return await _IProfileDataAccess.PutProfile(user);
        }

        public async Task<bool> IsAdmin(string userId)
        {
            return await _IProfileDataAccess.IsAdmin(userId);
        }
    }
}
