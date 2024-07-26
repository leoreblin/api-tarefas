using Microsoft.EntityFrameworkCore;

namespace Thunders.Tasks.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {            
        }


    }
}
