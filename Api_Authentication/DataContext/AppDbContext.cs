using Api_Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Authentication.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<PostType> postTypes { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<ConfirmEmail> confirmEmails { get; set; }
        public DbSet<Comments> comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = DESKTOP-533UHOP\\SQLEXPRESS; database = AuthenticationC#_API; integrated security = sspi; encrypt = true; trustservercertificate = true;");
        }
    }
}
