using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Room
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Room type is required")]
        public bool IsSpecial { get; set; }
    }
}
