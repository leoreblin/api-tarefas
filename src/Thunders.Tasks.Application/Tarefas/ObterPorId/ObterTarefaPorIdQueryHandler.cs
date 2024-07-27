using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Application.Tarefas.ObterPorId
{
    public sealed class ObterTarefaPorIdQueryHandler
        : IRequestHandler<ObterTarefaPorIdQuery, Result<Tarefa>>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public ObterTarefaPorIdQueryHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result<Tarefa>> Handle(
            ObterTarefaPorIdQuery request,
            CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.ObterPorIdAsync(request.Id);

            if (tarefa is null)
            {
                return Result.Fail<Tarefa>(TarefaError.NaoEncontrada);
            }

            return Result.Ok(tarefa);
        }
    }
}
