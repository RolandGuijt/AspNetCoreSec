using System.Security.Claims;
using System.Threading.Tasks;
using AspNetSecurity_m3.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AspNetSecurity_m3.Services
{
    public class ConfArchUserClaimsPrincipalFactory :
        UserClaimsPrincipalFactory<ConfArchUser, IdentityRole>
    {
        public ConfArchUserClaimsPrincipalFactory(
            UserManager<ConfArchUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ConfArchUser user)
        {
            var claimsPrincipal = await base.CreateAsync(user);
            var identity = claimsPrincipal.Identities.First();

            identity.AddClaim(new Claim("birthdate",
                user.BirthDate.ToString("MM/dd/yyyy")));

            return claimsPrincipal;
        }
    }
}
