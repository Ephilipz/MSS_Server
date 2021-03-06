using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class Billing
    {
        public int Id { get; set; }
        public string Address { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression(@"((0[0-9])|(1[0-2]))\/([0-9]{2})", ErrorMessage = "Invalid Expiry Date")]
        public string Expiry { get; set; }
    }
}
