using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Entities.Users;

namespace DataAccess.Profile
{
    public interface IProfileDataAccess
    {
        Task<IdentityUser> PostProfile(IdentityUser user);
        Task<IdentityUser> PutProfile(IdentityUser user);
        Task<IdentityUser> DeleteProfile(int id);
        Task<IdentityUser> GetProfile(string id);
        Task<Client> GetProfileWithBilling(string id);
        Task<List<Client>> GetProfiles();
        Task<bool> IsAdmin(string id);
    }
}
