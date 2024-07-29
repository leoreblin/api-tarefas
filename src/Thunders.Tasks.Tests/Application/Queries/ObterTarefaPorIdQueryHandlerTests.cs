using Thunders.Tasks.Application.Tarefas.ObterPorId;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Application.Queries
{
    public class ObterTarefaPorIdQueryHandlerTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;

        private readonly ObterTarefaPorIdQueryHandler _handler;

        public ObterTarefaPorIdQueryHandlerTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();

            _handler = new ObterTarefaPorIdQueryHandler(_mockTarefaRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoTarefaNaoForEncontrada()
        {
            // Arrange
            Tarefa? tarefa = null;
            var request = new ObterTarefaPorIdQuery(Guid.NewGuid());
            var cancellationToken = new CancellationToken();

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(request.Id, cancellationToken))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Failed.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(TarefaError.NaoEncontrada);
        }

        [Fact]
        public async Task Handle_DeveRetornarOk()
        {
            // Arrange
            var tarefa = Tarefa.Criar("Tarefa Fake", "");
            var request = new ObterTarefaPorIdQuery(tarefa.Id);
            var cancellationToken = new CancellationToken();

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(request.Id, cancellationToken))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Should().BeEquivalentTo(Result.Ok(tarefa));
        }
    }
}
