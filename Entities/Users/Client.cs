using Entities.Logging;
using Microsoft.AspNetCore.Identity;
using System;

namespace Entities.Users
{
    public class Client : IdentityUser
    {
        public virtual Billing BillingInformation { get; set; }
    }
}
