using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuickstartIdentityServer.Data
{
    public class UserRepository
    {
        private readonly UserDbContext dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> GetBySubjectId(Guid subjectId)
        {
            return 
                await dbContext.Users.SingleAsync(u => u.SubjectId == subjectId);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return
                await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
