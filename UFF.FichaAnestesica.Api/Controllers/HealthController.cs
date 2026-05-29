using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands;
using UFF.FichaAnestesica.Domain.Repositories.ReadOnly;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly IHealthReadOnlyRepository _repo;

        public HealthController(IHealthReadOnlyRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.CheckHealth();

            return Ok(new CommandResult(true, new
            {
                database = result.bd ? "online" : "offline",
                aghu = result.aghu ? "online" : "offline",
                timestamp = DateTime.UtcNow
            }));
        }
    }
}
