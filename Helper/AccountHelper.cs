using System;
using System.Security.Claims;

namespace Helper
{
    public static class AccountHelper
    {
        public static string getUserId(dynamic httpContext, ClaimsPrincipal User)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            string userId = string.Empty;
            if (identity != null)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return userId;
        }
    }
}
