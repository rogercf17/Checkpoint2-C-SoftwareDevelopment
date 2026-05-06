using Fiap.Banco.API.Data;
using Fiap.Banco.API.DTOs;
using Fiap.Banco.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Banco.API.Services
{
    public interface IClienteService
    {
        Task<ClienteResponse> CriarPessoaFisicaAsync(CriarPessoaFisicaRequest request);
        Task<ClienteResponse> CriarPessoaJuridicaAsync(CriarPessoaJuridicaRequest request);
        Task<ClienteResponse> BuscarPorIdAsync(int id);
    }

    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteResponse> CriarPessoaFisicaAsync(CriarPessoaFisicaRequest request)
        {
            var agenciaExiste = await _context.Agencias.CountAsync(a => a.Id == request.AgenciaId) > 0;
            if (!agenciaExiste) throw new KeyNotFoundException("Agencia não encontrada.");

            var cpfDuplicado = await _context.Clientes
                .OfType<PessoaFisica>()
                .CountAsync(p => p.CPF == request.Cpf) > 0;
            if (cpfDuplicado) throw new InvalidOperationException("CPF já cadastrado.");

            var pf = new PessoaFisica
            {
                Nome = request.Nome,
                CPF = request.Cpf,
                DataNascimento = request.DataNascimento,
                IdAgencia = request.AgenciaId
            };

            _context.Clientes.Add(pf);
            await _context.SaveChangesAsync();

            return new ClienteResponse(pf.Id, "PF", pf.Nome, pf.IdAgencia, pf.CPF, pf.DataNascimento, null, null);
        }

        public async Task<ClienteResponse> CriarPessoaJuridicaAsync(CriarPessoaJuridicaRequest request)
        {
            var agenciaExiste = await _context.Agencias.CountAsync(a => a.Id == request.AgenciaId) > 0;
            if (!agenciaExiste) throw new KeyNotFoundException("Agência não encontrada.");

            var cnpjDuplicado = await _context.Clientes
                .OfType<PessoaJuridica>()
                .CountAsync(p => p.CNPJ == request.Cnpj) > 0;
            if (cnpjDuplicado) throw new InvalidOperationException("CNPJ já cadastrado.");

            var pj = new PessoaJuridica
            {
                Nome = request.Nome,
                CNPJ = request.Cnpj,
                RazaoSocial = request.RazaoSocial,
                IdAgencia = request.AgenciaId
            };

            _context.Clientes.Add(pj);
            await _context.SaveChangesAsync();

            return new ClienteResponse(pj.Id, "PJ", pj.Nome, pj.IdAgencia, null, null, pj.CNPJ, pj.RazaoSocial);
        }

        public async Task<ClienteResponse?> BuscarPorIdAsync(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Agencia)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null) return null;

            return cliente switch
            {
                PessoaFisica pf => new ClienteResponse(pf.Id, "PF", pf.Nome, pf.IdAgencia,
                    pf.CPF, pf.DataNascimento, null, null),
                PessoaJuridica pj => new ClienteResponse(pj.Id, "PJ", pj.Nome, pj.IdAgencia,
                    null, null, pj.CNPJ, pj.RazaoSocial),
                _ => null
            };
        }
    }
}
