using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Core.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ListarAsync(CancellationToken cancellationToken = default);

        Task<Tarefa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task InserirAsync(Tarefa tarefa, CancellationToken cancellationToken = default);

        Task AtualizarAsync(Tarefa tarefa, CancellationToken cancellationToken = default);

        Task ExcluirAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
