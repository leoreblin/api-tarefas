using MediatR;
using Microsoft.AspNetCore.Mvc;
using Thunders.Tasks.Application.Tarefas.Completar;
using Thunders.Tasks.Application.Tarefas.Criar;
using Thunders.Tasks.Application.Tarefas.Excluir;
using Thunders.Tasks.Application.Tarefas.Listar;
using Thunders.Tasks.Application.Tarefas.ObterPorId;

namespace Thunders.Tasks.WebApi.Controllers
{
    [Route("api/v1/tarefas")]
    [ApiController]
    public sealed class TarefasController : ControllerBase
    {
        private readonly ISender _sender;

        public TarefasController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ListarTarefasQuery(), cancellationToken);

            if (result.Failed)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ObterTarefaPorIdQuery(id), cancellationToken);

            if (result.Failed)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarNovaTarefaCommand command,
            CancellationToken cancellationToken)
        {
            var tarefaCriadaResult = await _sender.Send(command, cancellationToken);

            if (tarefaCriadaResult.Failed)
            {
                return BadRequest(tarefaCriadaResult.Error);
            }

            return Ok(tarefaCriadaResult.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Completar(
            [FromBody] CompletarTarefaCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(command, cancellationToken);

            if (result.Failed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Excluir(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new ExcluirTarefaCommand(id), cancellationToken);

            if (result.Failed)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
