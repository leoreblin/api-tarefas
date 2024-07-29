using Thunders.Tasks.Application.Tarefas.Listar;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Application.Queries
{
    public class ListarTarefasQueryHandlerTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;

        private readonly ListarTarefasQueryHandler _handler;

        public ListarTarefasQueryHandlerTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();

            _handler = new ListarTarefasQueryHandler(_mockTarefaRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoNaoHouverTarefas()
        {
            // Arrange
            var tarefas = Enumerable.Empty<Tarefa>();
            var request = new ListarTarefasQuery();
            var cancellationToken = new CancellationToken();

            _mockTarefaRepository
                .Setup(m =>m.ListarAsync(cancellationToken))
                .ReturnsAsync(tarefas);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Failed.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(TarefaError.ListaVazia);
        }

        [Fact]
        public async Task Handle_DeveRetornarOk()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                Tarefa.Criar("Tarefa 1", ""),
                Tarefa.Criar("Tarefa 2", ""),
                Tarefa.Criar("Tarefa 3", "")
            }
            .AsEnumerable();

            var request = new ListarTarefasQuery();
            var cancellationToken = new CancellationToken();

            _mockTarefaRepository
                .Setup(m => m.ListarAsync(cancellationToken))
                .ReturnsAsync(tarefas);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Should().BeEquivalentTo(Result.Ok(tarefas));
        }
    }
}
