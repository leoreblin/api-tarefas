using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Infrastructure.Persistence.Configurations
{
    public class TarefaEntityTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));

            builder.ToTable("Tarefas");

            builder
                .HasKey(t => t.Id)
                .IsClustered(false)
                .HasName("PK_Tarefas");

            builder
                .Property(t => t.Id)
                .ValueGeneratedNever()
                .HasDefaultValueSql("newsequentialid()")
                .IsRequired();

            builder
                .Property(t => t.Nome)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder
                .Property(t => t.Descricao)
                .HasColumnType("nvarchar(100)")
                .IsRequired(false);

            builder
                .Property(t => t.DataCriacao)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(t => t.DataCompletou)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder
                .HasIndex(t => new
                {
                    t.Id
                })
                .IncludeProperties(t => new
                {
                    t.Nome,
                    t.Descricao
                })
                .HasDatabaseName("IDX_TAREFAS_ID");
        }
    }
}
