using MediatR;
using Thunders.Tasks.Core.Common;

namespace Thunders.Tasks.Application.Tarefas.Excluir
{
    public record ExcluirTarefaCommand(Guid Id) : IRequest<Result>;
}
