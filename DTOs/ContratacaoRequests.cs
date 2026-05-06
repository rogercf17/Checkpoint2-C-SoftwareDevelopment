using Fiap.Banco.API.Enums;

namespace Fiap.Banco.API.DTOs
{
    public class CriarContratacaoRequests
    {
        public int IdCliente { get; set; }
        public TipoProdouto TipoProduto { get; set; }
        public decimal? Valor { get; set; }
        public int? Parcelas { get; set; }
        public decimal? TaxaJuros { get; set; }
        public string? EmpresaCoveniada { get; set; }
        public int? ConvenioAtivo { get; set; }
    }

    public record ContratacaoResponse(
        int Id,
        int IdCliente,
        int IdProduto,
        string TipoProduto,
        string status,
        string? observacao,
        DateTime DataSolicitacao,
        DateTime? DataProcessamento
    );
}
