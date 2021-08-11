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
        [Required]
        [RegularExpression(@"[0-9]{9}", ErrorMessage = "Invalid Client ID")]
        public int ClientId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        public string Expiry { get; set; }
    }
}
