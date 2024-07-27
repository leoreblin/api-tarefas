using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;

namespace Thunders.Tasks.Application.Tarefas.Completar
{
    public sealed class CompletarTarefaCommandHandler
        : IRequestHandler<CompletarTarefaCommand, Result>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public CompletarTarefaCommandHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result> Handle(
            CompletarTarefaCommand request,
            CancellationToken cancellationToken)
        {
            var tarefaFromDb = await _tarefaRepository.ObterPorIdAsync(request.Id, cancellationToken);

            if (tarefaFromDb is null)
            {
                return Result.Fail(TarefaError.NaoEncontrada);
            }

            if (tarefaFromDb.EstaCompleta)
            {
                return Result.Ok();
            }

            tarefaFromDb.Completar();

            await _tarefaRepository.AtualizarAsync(tarefaFromDb, cancellationToken);

            return Result.Ok();
        }
    }
}
