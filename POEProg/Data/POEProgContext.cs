using Microsoft.EntityFrameworkCore;
using POEProg.Models;

namespace POEProg.Data
{
    public class POEProgContext : DbContext
    {
        public POEProgContext(DbContextOptions<POEProgContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Document> Documents { get; set; }
        // Add other DbSets here (e.g., Users)
    }
}
