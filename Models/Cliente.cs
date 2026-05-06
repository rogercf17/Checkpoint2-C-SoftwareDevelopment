using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.Models
{
    public abstract class Cliente
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        public int IdAgencia { get; set; }
        public Agencia? Agencia { get; set; }

        public List<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();
    }
}
