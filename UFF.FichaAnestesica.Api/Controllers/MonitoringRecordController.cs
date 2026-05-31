using Microsoft.AspNetCore.Mvc;
using UFF.FichaAnestesica.Domain.Commands.AnesthesiaRecord;
using UFF.FichaAnestesica.Domain.Services;

namespace UFF.FichaAnestesica.Api.Controllers
{
    [ApiController]
    [Route("api/monitoring")]
    public class MonitoringRecordController : ControllerBase
    {
        private readonly IMonitoringRecordService _monitoringRecordService;

        public MonitoringRecordController(IMonitoringRecordService monitoringRecordService)
        {
            _monitoringRecordService = monitoringRecordService;
        }

        /// <summary>
        /// Obtém um registro de monitorização por ID
        /// </summary>
        /// <param name="id">ID do registro de monitorização</param>
        /// <returns>Registro de monitorização encontrado</returns>
        [HttpGet("{id}")]
        // [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _monitoringRecordService.GetByIdAsync(id);

            if (!result.Valid)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Cria um novo registro de monitorização
        /// </summary>
        /// <param name="command">Dados do registro de monitorização</param>
        /// <returns>Registro criado</returns>
        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Create([FromBody] MonitoringRecordCommand command)
        {
            var result = await _monitoringRecordService.Create(command);

            if (!result.Valid)
                return BadRequest(result);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Atualiza um registro de monitorização existente
        /// </summary>
        /// <param name="id">ID do registro a ser atualizado</param>
        /// <param name="command">Dados atualizados</param>
        /// <returns>Registro atualizado</returns>
        [HttpPut("{id}")]
        // [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MonitoringRecordCommand command)
        {
            if (command == null)
                return BadRequest(command);

            var result = await _monitoringRecordService.Update(id, command);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }

        // <summary>
        /// Finaliza o monitoramento de uma cirurgia em andamento
        /// </summary>
        /// <param name="id">ID do registro a ser finalizado</param>
        /// <param name="command">Dados atualizados</param>
        /// <returns>Registro atualizado</returns>
        [HttpPatch("{id}")]
        // [Authorize]
        public async Task<IActionResult> FinalizePatientAsync([FromRoute] int id)
        {
            var result = await _monitoringRecordService.FinalizePatientAsync(id);

            if (!result.Valid)
                return BadRequest(result);

            return Ok(result);
        }
    }
}