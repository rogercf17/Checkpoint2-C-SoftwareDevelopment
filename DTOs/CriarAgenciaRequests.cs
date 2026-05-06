using System.ComponentModel.DataAnnotations;

namespace Fiap.Banco.API.DTOs
{
    public record CriarAgenciaRequests(
        [Required] string Numero,
        [Required] string Nome,
        string Cidade
    );

    public record AgenciaResponse(
        int Id,
        string Numero,
        string Nome,
        string Cidade
    );
}
