using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Authentication
{
    public class RegisterClientVM
    {
        [Required(ErrorMessage = "Email is required")]
        //RegEx: start with string and end with string. Email address must have @psu.edu domain
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*)@psu.edu\Z",
            ErrorMessage = "Invalid Email Domain")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        //RegEx: Id number must have 9 digits
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "Invalid User ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
