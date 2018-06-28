using System.Threading;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetSecurity_m3.Data
{
    public class ConfArchDbContext : IdentityDbContext<ConfArchUser>
    {
        public ConfArchDbContext(DbContextOptions<ConfArchDbContext> options)
            : base(options)
        {
            
        }
    }
}
