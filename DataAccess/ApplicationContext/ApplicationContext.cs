using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Logging;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) { }

        //This function is used to map the classes to mySQL tables
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LogItem>().ToTable("Logging");
            builder.Entity<Room>().ToTable("Room");
            builder.Entity<Entities.Reservation>().ToTable("Reservation");
            builder.Entity<Entities.Complaints.Complaint>().ToTable("Complaint");
            base.OnModelCreating(builder);
        }


        //each entity class must be mapped to a DbSet below in order to be added to the database
        public DbSet<LogItem> Logging { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Entities.Reservation> Reservations { get; set; }
        public DbSet<Entities.Complaints.Complaint> Complaints { get; set; }
    }
}
