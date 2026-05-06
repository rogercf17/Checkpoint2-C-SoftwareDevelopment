using Fiap.Banco.API.DTOs;
using Fiap.Banco.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Banco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgenciasController : ControllerBase
    {
        private readonly IAgenciaService _service;

        public AgenciasController(IAgenciaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAgenciaRequests request)
        {
            try
            {
                var result = await _service.CriarAsync(request);
                return CreatedAtAction(nameof(BuscarPorId), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var result = await _service.BuscarPorIdAsync(id);

            if (result == null)
                return NotFound(new { mensagem = "Agência não encontrada." });

            return Ok(result);
        }
    }
}