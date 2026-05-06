using Fiap.Banco.API.Data;
using Fiap.Banco.API.DTOs;
using Fiap.Banco.API.Models;

namespace Fiap.Banco.API.Services
{
    public interface IAgenciaService
    {
        Task<AgenciaResponse> CriarAsync(CriarAgenciaRequests request);
        Task<AgenciaResponse> BuscarPorIdAsync(int id);
    }

    public class AgenciaService : IAgenciaService
    {
        private readonly AppDbContext _context;

        public AgenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AgenciaResponse> CriarAsync(CriarAgenciaRequests request)
        {
            var agencia = new Agencia
            {
                Numero = request.Numero,
                Nome = request.Nome,
                Cidade = request.Cidade
            };

            _context.Agencias.Add(agencia);
            await _context.SaveChangesAsync();

            return new AgenciaResponse(agencia.Id, agencia.Numero, agencia.Nome, agencia.Cidade);
        }

        public async Task<AgenciaResponse> BuscarPorIdAsync(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);

            if (agencia is null) return null;

            return new AgenciaResponse(agencia.Id, agencia.Numero, agencia.Nome, agencia.Cidade);
        }
    }
}
