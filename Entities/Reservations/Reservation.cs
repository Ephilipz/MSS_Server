using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Host is required")]
        public IdentityUser User { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        public Room Room { get; set; }

        [Required(ErrorMessage = "Start date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End date and time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }

        public string Participants { get; set; }
    }
}
