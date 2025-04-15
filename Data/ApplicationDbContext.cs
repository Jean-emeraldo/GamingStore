using Microsoft.EntityFrameworkCore;
using GamingStore.Models;

namespace GamingStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
