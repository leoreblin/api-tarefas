using Thunders.Tasks.Application.Tarefas.Excluir;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Application.Commands
{
    public class ExcluirTarefaCommandHandlerTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;

        private readonly ExcluirTarefaCommandHandler _handler;

        public ExcluirTarefaCommandHandlerTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();

            _handler = new ExcluirTarefaCommandHandler(_mockTarefaRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoTarefaNaoForEncontrada()
        {
            // Arrange
            Tarefa? tarefa = null;
            var request = new ExcluirTarefaCommand(Guid.NewGuid());
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
            var tarefa = Tarefa.Criar("Tarefa Fake", string.Empty);
            var request = new ExcluirTarefaCommand(tarefa.Id);
            var cancellationToken = new CancellationToken();

            _mockTarefaRepository
                .Setup(m => m.ObterPorIdAsync(request.Id, cancellationToken))
                .ReturnsAsync(tarefa);

            _mockTarefaRepository
                .Setup(m => m.ExcluirAsync(request.Id, cancellationToken));

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Should().BeEquivalentTo(Result.Ok());
        }
    }
}
