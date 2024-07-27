using MediatR;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Application.Tarefas.Listar
{
    public record ListarTarefasQuery : IRequest<Result<IEnumerable<Tarefa>>>;
}
