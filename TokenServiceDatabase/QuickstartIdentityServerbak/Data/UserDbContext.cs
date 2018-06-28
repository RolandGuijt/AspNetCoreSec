using Microsoft.EntityFrameworkCore;

namespace QuickstartIdentityServer.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(i => i.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(i => i.SubjectId).IsUnique();
        }
    }
}
