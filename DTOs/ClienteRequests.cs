using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.DTOs
{
    public record CriarPessoaFisicaRequest([Required] string Nome, [Required] string Cpf, DateTime DataNascimento, int AgenciaId);
    public record CriarPessoaJuridicaRequest([Required] string Nome, [Required] string Cnpj, [Required] string RazaoSocial, int AgenciaId);
    public record ClienteResponse(int Id, string Tipo, string Nome, int AgenciaId, string? Cpf, DateTime? DataNascimento, string? Cnpj, string? RazaoSocial);
}
