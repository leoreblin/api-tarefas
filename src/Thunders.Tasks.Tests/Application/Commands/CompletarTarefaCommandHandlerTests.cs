using Thunders.Tasks.Application.Tarefas.Completar;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Application.Commands
{
    public class CompletarTarefaCommandHandlerTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;

        private readonly CompletarTarefaCommandHandler _handler;

        public CompletarTarefaCommandHandlerTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();

            _handler = new CompletarTarefaCommandHandler(_mockTarefaRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoTarefaNaoForEncontradaPeloId()
        {
            // Arrange
            Tarefa? tarefa = null;
            var cancellationToken = new CancellationToken();
            var request = new CompletarTarefaCommand(Guid.NewGuid());

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(It.IsAny<Guid>(), cancellationToken))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Failed.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(TarefaError.NaoEncontrada);
        }

        [Fact]
        public async Task Handle_DeveRetornarOk_QuandoTarefaJaEstiverCompletada()
        {
            // Arrange
            var tarefa = Tarefa.Criar("Tarefa Completa", "A tarefa já encontra-se completa");
            tarefa.Completar();
            var cancellationToken = new CancellationToken();
            var request = new CompletarTarefaCommand(tarefa.Id);

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(tarefa.Id, cancellationToken))
                .ReturnsAsync(tarefa);

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Should().BeEquivalentTo(Result.Ok());
        }

        [Fact]
        public async Task Handle_DeveRetornarOk()
        {
            // Arrange
            var tarefa = Tarefa.Criar("Tarefa Ainda Não Completa", "");
            var cancellationToken = new CancellationToken();
            var request = new CompletarTarefaCommand(tarefa.Id);

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(tarefa.Id, cancellationToken))
                .ReturnsAsync(tarefa);

            _mockTarefaRepository
                .Setup(m => m.AtualizarAsync(tarefa, cancellationToken));

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Should().BeEquivalentTo(Result.Ok());
        }
    }
}
