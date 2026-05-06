namespace Fiap.Banco.Api.Models;

public class Emprestimo : Produto
{
    public decimal Valor { get; set; }
    public int Parcelas { get; set; }
    public decimal TaxaJuros { get; set; }
}
