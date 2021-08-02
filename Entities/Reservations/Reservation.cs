using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    class Reservation
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public Room Room { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Boolean isPayed { get; set; }
        public List<IdentityUser> Participants { get; set; }
    }
}
