using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thunders.Tasks.Application.Tarefas.Completar;
using Thunders.Tasks.Application.Tarefas.Criar;
using Thunders.Tasks.Application.Tarefas.Excluir;
using Thunders.Tasks.Application.Tarefas.Listar;
using Thunders.Tasks.Application.Tarefas.ObterPorId;
using Thunders.Tasks.Core.Common;
using Thunders.Tasks.Core.Errors;
using Thunders.Tasks.Domain.Entities;
using Thunders.Tasks.WebApi.Controllers;

namespace Thunders.Tasks.Tests.Controllers
{
    public class TarefasControllerTests
    {
        private readonly Mock<ISender> _mockSender;

        private readonly TarefasController _tarefasController;

        public TarefasControllerTests()
        {
            _mockSender = new Mock<ISender>();

            _tarefasController = new TarefasController(_mockSender.Object);
        }

        [Fact]
        public async Task Listar_DeveRetornarNotFound_QuandoNaoHouverTarefas()
        {
            // Arrange
            var result = Result.Fail<IEnumerable<Tarefa>>(TarefaError.ListaVazia);
            var cancellationToken = new CancellationToken();

            _mockSender
                .Setup(m => m.Send(It.IsAny<ListarTarefasQuery>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Listar(cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new NotFoundObjectResult(result.Error)
            {
                StatusCode = StatusCodes.Status404NotFound
            });
        }

        [Fact]
        public async Task Listar_DeveRetornarOk()
        {
            // Arrange
            var tarefas = new List<Tarefa>
            {
                Tarefa.Criar("Organizar quarto", "Cama e guarda-roupa"),
                Tarefa.Criar("Estudar", "Matemática"),
                Tarefa.Criar("Fazer compra", ""),
            };

            var result = Result.Ok(tarefas.AsEnumerable());

            var cancellationToken = new CancellationToken();

            _mockSender
                .Setup(m => m.Send(It.IsAny<ListarTarefasQuery>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Listar(cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new OkObjectResult(result.Value)
            {
                StatusCode = StatusCodes.Status200OK
            });
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNotFound_QuandoNaoEncontrarTarefaPeloId()
        {
            // Arrange
            var result = Result.Fail<Tarefa>(TarefaError.NaoEncontrada);
            var cancellationToken = new CancellationToken();

            _mockSender
                .Setup(m => m.Send(It.IsAny<ObterTarefaPorIdQuery>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.ObterPorId(Guid.NewGuid(), cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new NotFoundObjectResult(result.Error)
            {
                StatusCode = StatusCodes.Status404NotFound
            });
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarOk()
        {
            // Arrange
            var tarefa = Tarefa.Criar("Organizar quarto", "Cama e guarda-roupa");
            var result = Result.Ok(tarefa);
            var cancellationToken = new CancellationToken();

            _mockSender
                .Setup(m => m.Send(It.IsAny<ObterTarefaPorIdQuery>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.ObterPorId(tarefa.Id, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new OkObjectResult(result.Value)
            {
                StatusCode = StatusCodes.Status200OK
            });
        }

        [Fact]
        public async Task Criar_DeveRetornarBadRequest_QuandoHouverErroAoCriarNovaTarefa()
        {
            // Arrange
            var error = new Error("Erro ao criar nova tarefa");
            var result = Result.Fail<TarefaCriadaResponse>(error);
            var cancellationToken = new CancellationToken();
            var command = new CriarNovaTarefaCommand("Tarefa 1", "");

            _mockSender
                .Setup(m => m.Send(It.IsAny<CriarNovaTarefaCommand>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Criar(command, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new BadRequestObjectResult(result.Error)
            {
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        [Fact]
        public async Task Criar_DeveRetornarOk()
        {
            // Arrange
            var tarefaCriadaResponse = new TarefaCriadaResponse(Guid.NewGuid());
            var result = Result.Ok(tarefaCriadaResponse);
            var cancellationToken = new CancellationToken();
            var command = new CriarNovaTarefaCommand("Tarefa 1", "");

            _mockSender
                .Setup(m => m.Send(It.IsAny<CriarNovaTarefaCommand>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Criar(command, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new OkObjectResult(result.Value)
            {
                StatusCode = StatusCodes.Status200OK
            });
        }

        [Fact]
        public async Task Completar_DeveRetornarBadRequest_QuandoHouverErro()
        {
            // Arrange
            var error = new Error("Erro ao completar tarefa");
            var result = Result.Fail(error);
            var cancellationToken = new CancellationToken();
            var command = new CompletarTarefaCommand(Guid.NewGuid());

            _mockSender
                .Setup(m => m.Send(It.IsAny<CompletarTarefaCommand>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Completar(command, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new BadRequestObjectResult(result.Error)
            {
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        [Fact]
        public async Task Completar_DeveRetornarOk()
        {
            // Arrange
            var result = Result.Ok();
            var cancellationToken = new CancellationToken();
            var command = new CompletarTarefaCommand(Guid.NewGuid());

            _mockSender
                .Setup(m => m.Send(It.IsAny<CompletarTarefaCommand>(), cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Completar(command, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new OkResult());
        }

        [Fact]
        public async Task Excluir_DeveRetornarBadRequest_QuandoHouverErro()
        {
            // Arrange
            var error = new Error("Erro ao excluir tarefa");
            var result = Result.Fail(error);
            var cancellationToken = new CancellationToken();
            var tarefaId = Guid.NewGuid();
            var command = new ExcluirTarefaCommand(tarefaId);

            _mockSender
                .Setup(m => m.Send(command, cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Excluir(tarefaId, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new BadRequestObjectResult(result.Error)
            {
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        [Fact]
        public async Task Excluir_DeveRetornarOk()
        {
            // Arrange
            var result = Result.Ok();
            var cancellationToken = new CancellationToken();
            var tarefaId = Guid.NewGuid();
            var command = new ExcluirTarefaCommand(tarefaId);

            _mockSender
                .Setup(m => m.Send(command, cancellationToken))
                .ReturnsAsync(result);

            // Act
            IActionResult actionResult = await _tarefasController.Excluir(tarefaId, cancellationToken);

            // Assert
            actionResult.Should().BeEquivalentTo(new OkResult());
        }
    }
}
