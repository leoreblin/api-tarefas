using Microsoft.AspNetCore.Mvc;

namespace Thunders.Tasks.WebApi.Controllers
{
    [Route("api/v1/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {


            return NotFound();
        }
    }
}
