using MediatR;
using Thunders.Tasks.Core.Common;

namespace Thunders.Tasks.Application.Tarefas.Completar
{
    public record CompletarTarefaCommand(Guid Id) : IRequest<Result>;
}
