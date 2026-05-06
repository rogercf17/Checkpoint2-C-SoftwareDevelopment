using Fiap.Banco.API.DTOs;
using Fiap.Banco.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Banco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost("pf")]
        public async Task<IActionResult> CriarPF([FromBody] CriarPessoaFisicaRequest request)
        {
            try
            {
                var result = await _service.CriarPessoaFisicaAsync(request);
                return CreatedAtAction(nameof(BuscarPorId), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("pj")]
        public async Task<IActionResult> CriarPJ([FromBody] CriarPessoaJuridicaRequest request)
        {
            try
            {
                var result = await _service.CriarPessoaJuridicaAsync(request);
                return CreatedAtAction(nameof(BuscarPorId), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var result = await _service.BuscarPorIdAsync(id);

            if (result == null)
                return NotFound(new { mensagem = "Cliente não encontrado." });

            return Ok(result);
        }
    }
}