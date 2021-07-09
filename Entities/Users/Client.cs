using Entities.Logging;
using Microsoft.AspNetCore.Identity;
using System;

namespace Entities.Users
{
    public class Client : IdentityUser
    {
        public virtual Billing BillingInformation { get; set; }

        public void UpdateBillingInformation(Billing billingInformation, Administrator admin = null)
        {
            IdentityUser user = admin == null ? this : admin;
            LogItem log = new LogItem(DateTime.UtcNow, "Billing", user.UserName + " changed billing data of " + this.UserName);
        }
    }
}
