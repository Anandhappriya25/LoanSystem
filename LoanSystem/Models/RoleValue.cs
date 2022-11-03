using System.Security.Claims;
using System.Security.Principal;

namespace LoanSystem.Models
{
    public static class RoleValue
    {
        public static string GetClaimValue(this IIdentity identity, string key)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim role = claimsIdentity.FindFirst(ClaimTypes.Role);
            Claim name = claimsIdentity.FindFirst(ClaimTypes.Name);
            Claim id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Claim email = claimsIdentity.FindFirst(ClaimTypes.Email);
            if (key == "name")
            {
                return name.Value;
            }
            if (key == "id")
            {
                return id.Value;
            }
            if (key == "email")
            {
                return email.Value;
            }
            else
            {
                return role.Value;
            }
        }
    }
}
