using Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Profile
{
    public interface IProfileDataService
    {
        Task<IdentityUser> PostProfile(IdentityUser user);
        Task<IdentityUser> PutProfile(IdentityUser user);
        Task<IdentityUser> DeleteProfile(int id);
        Task<IdentityUser> GetProfile(int id);
        Task<List<IdentityUser>> GetProfiles();
    }
}
