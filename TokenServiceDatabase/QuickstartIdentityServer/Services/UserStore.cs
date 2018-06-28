using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using QuickstartIdentityServer.Data;

namespace QuickstartIdentityServer.Services
{
    public class UserStore
    {
        private readonly UserRepository userRepository;

        public UserStore(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> FindBySubjectId(string subjectId)
        {
            return
                await userRepository.GetBySubjectId(Guid.Parse(subjectId));
        }

        public async Task<User> FindByUsername(string userName)
        {
            return
                await userRepository.GetByUserName(userName);
        }

        public async Task<bool> ValidateCredentials(string userName, string password)
        {
            var user = await userRepository.GetByUserName(userName);
            if (user == null)
                return false;
            return user.Password == password.Sha256();
        }

        public async Task<User> AutoProvisionUser(string providerName, string providerSubjectId,
            List<Claim> providerClaims)
        {
            var name = providerClaims.SingleOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            name = name ?? providerClaims.SingleOrDefault(c => c.Type == JwtClaimTypes.FamilyName)?.Value;
            var webSite = providerClaims.SingleOrDefault(c => c.Type == ClaimTypes.Webpage)?.Value;

            var newSubjectId = Guid.NewGuid();

            var userName = name ?? newSubjectId.ToString();

            var newUser = new User
            {
                SubjectId = newSubjectId,
                IsActive = true,
                UserName = userName,
                Name = name,
                Website = webSite,
                Password = ""
            };

            await userRepository.CreateUser(newUser, providerName, providerSubjectId);
            return newUser;
        }

        public async Task<User> FindByExternalProvider(string providerName, string subjectId)
        {
            return await userRepository.GetByExternalProviderInfo(providerName, subjectId);
        }
    }
}
