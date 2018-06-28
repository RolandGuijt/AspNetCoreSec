using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using QuickstartIdentityServer.Data;
using IdentityServer4.Extensions;

namespace QuickstartIdentityServer.Services
{
    public class ProfileService: IProfileService
    {
        private readonly UserRepository userRepository;

        public ProfileService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await userRepository.GetBySubjectId(Guid.Parse(subjectId));

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.Name ?? subjectId),
                new Claim(JwtClaimTypes.WebSite, user.Website ?? "")
            };

            context.AddRequestedClaims(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await userRepository.GetBySubjectId(Guid.Parse(subjectId));
            context.IsActive = user.IsActive;
        }
    }
}
