using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Infrastructure.Persistence.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarAsync(Tarefa tarefa, CancellationToken cancellationToken)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirAsync(Guid id, CancellationToken cancellationToken)
        {
            var tarefa = await _context.Tarefas.FindAsync(id , cancellationToken);
            if (tarefa is not null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task InserirAsync(Tarefa tarefa, CancellationToken cancellationToken)
        {
            await _context.Tarefas.AddAsync(tarefa, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tarefa>> ListarAsync(CancellationToken cancellationToken)
        {
            return await _context.Tarefas.ToListAsync(cancellationToken);
        }

        public async Task<Tarefa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tarefas.FindAsync(id, cancellationToken);
        }
    }
}
