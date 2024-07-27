using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Application.Tarefas.Criar
{
    public sealed class CriarNovaTarefaCommandHandler
        : IRequestHandler<CriarNovaTarefaCommand, Result<TarefaCriadaResponse>>
    {
        private readonly ITarefaRepository _tarefaRepository;

        public CriarNovaTarefaCommandHandler(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Result<TarefaCriadaResponse>> Handle(
            CriarNovaTarefaCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Nome))
            {
                return Result.Fail<TarefaCriadaResponse>(TarefaError.NomeObrigatorio);
            }

            Tarefa tarefaCriada = Tarefa.Criar(request.Nome, request.Descricao);

            await _tarefaRepository.InserirAsync(tarefaCriada, cancellationToken);

            return Result.Ok(new TarefaCriadaResponse(tarefaCriada.Id));
        }
    }
}
