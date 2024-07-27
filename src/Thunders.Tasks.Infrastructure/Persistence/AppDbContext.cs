using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.Domain.Entities;
using Thunders.Tasks.Infrastructure.Persistence.Configurations;

namespace Thunders.Tasks.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaEntityTypeConfiguration());
        }
    }
}
