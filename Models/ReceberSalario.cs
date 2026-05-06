using Fiap.Banco.Api.Models;

namespace Fiap.Banco.API.Models
{
    public class ReceberSalario : Produto
    {
        public string EmpresaConveniada { get; set; } = string.Empty;

        public int ConvenioAtivo { get; set; }
    }
}
