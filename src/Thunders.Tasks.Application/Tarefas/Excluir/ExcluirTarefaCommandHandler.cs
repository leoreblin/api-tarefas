using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Repositories;

namespace Thunders.Tasks.Application.Tarefas.Excluir
{
    public sealed class ExcluirTarefaCommandHandler
        : IRequestHandler<ExcluirTarefaCommand, Result>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public ExcluirTarefaCommandHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result> Handle(
            ExcluirTarefaCommand request,
            CancellationToken cancellationToken)
        {
            await _tarefaRepository.ExcluirAsync(request.Id, cancellationToken);
            return Result.Ok();
        }
    }
}
