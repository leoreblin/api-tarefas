using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Application.Tarefas.ObterPorId
{
    public record ObterTarefaPorIdQuery(Guid Id) : IRequest<Result<Tarefa>>;
}
