using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class Billing
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
    }
}
