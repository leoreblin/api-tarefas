using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Application.Tarefas.Listar
{
    public sealed class ListarTarefasQueryHandler
        : IRequestHandler<ListarTarefasQuery, Result<IEnumerable<Tarefa>>>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public ListarTarefasQueryHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result<IEnumerable<Tarefa>>> Handle(
            ListarTarefasQuery request,
            CancellationToken cancellationToken)
        {
            var tarefas = await _tarefaRepository.ListarAsync();

            if (!tarefas.Any())
            {
                return Result.Fail<IEnumerable<Tarefa>>(TarefaError.ListaVazia);
            }

            return Result.Ok(tarefas);
        }
    }
}
