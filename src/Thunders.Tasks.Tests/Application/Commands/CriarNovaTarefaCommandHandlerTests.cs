using Thunders.Tasks.Application.Tarefas.Criar;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Domain.Entities;

namespace Thunders.Tasks.Tests.Application.Commands
{
    public class CriarNovaTarefaCommandHandlerTests
    {
        private readonly Mock<ITarefaRepository> _mockTarefaRepository;

        private readonly CriarNovaTarefaCommandHandler _handler;

        public CriarNovaTarefaCommandHandlerTests()
        {
            _mockTarefaRepository = new Mock<ITarefaRepository>();

            _handler = new CriarNovaTarefaCommandHandler(_mockTarefaRepository.Object);
        }

        [Fact]
        public async Task Handle_DeveRetornarErro_QuandoNomeNaoForInformado()
        {
            // Arrange
            var request = new CriarNovaTarefaCommand(string.Empty, string.Empty);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Failed.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(TarefaError.NomeObrigatorio);
        }

        [Fact]
        public async Task Handle_DeveRetornarOk()
        {
            // Arrange
            var nome = "Tarefa Fake";
            var descricao = "Descricao fake";
            var request = new CriarNovaTarefaCommand(nome, descricao);
            var cancellationToken = new CancellationToken();
            var tarefaCriada = Tarefa.Criar(nome, descricao);
            var tarefaCriadaResponse = new TarefaCriadaResponse(tarefaCriada.Id);

            _mockTarefaRepository
                .Setup(m => m.InserirAsync(tarefaCriada, cancellationToken));

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }
    }
}
