using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        public User AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public User FindByExternalProvider(string provider, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
