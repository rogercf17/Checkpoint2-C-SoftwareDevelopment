using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.Models
{
    public class PessoaJuridica : Cliente
    {
        [Required, MaxLength(14)]
        public string CNPJ { get; set; } = string.Empty;

        [Required, MaxLength(180)]
        public string RazaoSocial { get; set; } = string.Empty;
    }
}
