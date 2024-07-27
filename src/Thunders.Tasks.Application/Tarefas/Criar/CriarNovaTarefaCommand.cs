using MediatR;
using Thunders.Tasks.Core.Common;

namespace Thunders.Tasks.Application.Tarefas.Criar
{
    public record CriarNovaTarefaCommand(
        string Nome,
        string Descricao) : IRequest<Result<TarefaCriadaResponse>>;
}
