using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.Models
{
    public class Agencia
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Numero { get; set; } = string.Empty;

        [Required, MaxLength(120)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(120)]
        public string Cidade { get; set; } = string.Empty;

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
