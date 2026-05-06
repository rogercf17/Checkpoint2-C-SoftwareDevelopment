using Fiap.Banco.Api.Models;
using Fiap.Banco.API.Enums;

namespace Fiap.Banco.API.Models
{
    public class Contratacao
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }
        public Cliente? Cliente { get; set; }

        public int IdProduto { get; set; }
        public Produto? Produto { get; set; }

        public ContratacaoStatus Status { get; set; } = ContratacaoStatus.PendenteProcessamento;
        public string? observacao { get; set; }

        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataProcessamento { get; set; }
    }
}
