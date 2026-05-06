using Fiap.Banco.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.Api.Models;

public abstract class Produto
{
    public int Id { get; set; }

    [Required, MaxLength(80)]
    public string Nome { get; set; } = string.Empty;

    public List<Contratacao> Contratacoes { get; set; } = new List<Contratacao>();
}
