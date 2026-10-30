using Microsoft.EntityFrameworkCore;

namespace RESTbottle2.Models
{
    public class BottlesDbContext: DbContext
    {
        public BottlesDbContext(DbContextOptions<BottlesDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bottle> Bottles { get; set; }
    }
}
