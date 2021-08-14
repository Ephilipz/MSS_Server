using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Profile
{
    public class ProfileDataAccess : IProfileDataAccess
    {
        private readonly ApplicationContext _context;
        public ProfileDataAccess(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IdentityUser> DeleteProfile(int id)
        {
            //get the profile from the database
            IdentityUser user = await _context.Users.FindAsync(id);

            //if the user exists, delete it
            if (user != null)
            {
                _context.Remove(user);
            }

            //save changes
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IdentityUser> GetProfile(string id)
        {
            //get the user from database
            IdentityUser user = await _context.Users.FirstAsync(profile => profile.Id == id);
            return user;
        }

        public async Task<Client> GetProfileWithBilling(string id)
        {
            return await _context.Clients.Where(client => client.Id == id)
                .Include(client => client.BillingInformation).FirstOrDefaultAsync();
        }

        public async Task<List<Client>> GetProfiles()
        {
            //get the all users from database
            return await _context.Clients.ToListAsync();
        }

        public async Task<IdentityUser> PostProfile(IdentityUser user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IdentityUser> PutProfile(IdentityUser user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsAdmin(string userId)
        {
            return await _context.Administrators.AnyAsync(admin => admin.Id == userId);
        }
    }
}
