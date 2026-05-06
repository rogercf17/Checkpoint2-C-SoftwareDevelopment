using Fiap.Banco.API.Data;
using Fiap.Banco.API.DTOs;
using Fiap.Banco.API.Enums;
using Fiap.Banco.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Banco.API.Services
{
    public interface IContratacaoService
    {
        Task<ContratacaoResponse> CriarAsync(CriarContratacaoRequests request);
        Task<ContratacaoResponse?> BuscarPorIdAsync(int id);
    }

    public class ContratacaoService : IContratacaoService
    {
        private readonly AppDbContext _context;

        public ContratacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ContratacaoResponse> CriarAsync(CriarContratacaoRequests request)
        {
           var clienteExiste = await _context.Clientes
                .CountAsync(c => c.Id == request.IdCliente) > 0;
           if(!clienteExiste) throw new KeyNotFoundException("Cliente não encontrado.");

           var produto = await _context.Produtos
                .FirstOrDefaultAsync(p =>
                    EF.Property<string>(p, "TipoProduto") == request.TipoProduto.ToString());
           if (produto == null) throw new KeyNotFoundException("Produto não encontrado.");

            var contratacao = new Contratacao
            {
                IdCliente = request.IdCliente,
                IdProduto = produto.Id,
                Status = ContratacaoStatus.PendenteProcessamento,
                DataSolicitacao = DateTime.UtcNow
            };

            _context.Contratacoes.Add(contratacao);
            await _context.SaveChangesAsync();

            await ProcessarAsync(contratacao);

            return new ContratacaoResponse(
                contratacao.Id,
                contratacao.IdCliente,
                contratacao.IdProduto,
                request.TipoProduto.ToString(),
                contratacao.Status.ToString(),
                contratacao.observacao,
                contratacao.DataSolicitacao,
                contratacao.DataProcessamento ?? DateTime.UtcNow
            );
        }

        private async Task ProcessarAsync(Contratacao contratacao)
        {
            var aprovado = new Random().Next(0, 2) == 1;

            contratacao.Status = aprovado
                ? ContratacaoStatus.Aprovada
                : ContratacaoStatus.Rejeitada;

            contratacao.observacao = aprovado
                ? "Contratação aprovada com sucesso."
                : "Contratação recusada após análise.";

            contratacao.DataProcessamento = DateTime.UtcNow;

            _context.Contratacoes.Update(contratacao);
            await _context.SaveChangesAsync();
        }

        public async Task<ContratacaoResponse?> BuscarPorIdAsync(int id)
        {
            var contratacao = await _context.Contratacoes
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contratacao == null) return null;

            return new ContratacaoResponse(
                contratacao.Id,
                contratacao.IdCliente,
                contratacao.IdProduto,
                contratacao.Produto != null
                    ? _context.Entry(contratacao.Produto).Property("TipoProduto").CurrentValue?.ToString() ?? ""
                    : "",
                contratacao.Status.ToString(),
                contratacao.observacao,
                contratacao.DataSolicitacao,
                contratacao.DataProcessamento ?? DateTime.UtcNow
            );
        }
    }
}
