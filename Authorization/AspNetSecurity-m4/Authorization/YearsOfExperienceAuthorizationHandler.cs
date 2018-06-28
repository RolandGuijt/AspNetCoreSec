using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetSecurity_m4.Authorization
{
    public class YearsOfExperienceAuthorizationHandler: AuthorizationHandler<YearsOfExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            YearsOfExperienceRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "yearsofexperience"))
                return Task.CompletedTask;

            var yearsOfExperience = int.Parse(
                context.User.FindFirst(c => c.Type == "yearsofexperience").Value
            );

            if (yearsOfExperience >= requirement.YearsOfExperienceRequired)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
