using Microsoft.EntityFrameworkCore;

namespace JazzcashPortal.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> TBL_USERS { get; set; }
    }
}
