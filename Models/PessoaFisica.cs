using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.Models
{
    public class PessoaFisica : Cliente
    {
        [Required, MaxLength(11)]
        public string CPF { get; set; } = string.Empty;

        public DateTime DataNascimento { get; set; }
    }
}
