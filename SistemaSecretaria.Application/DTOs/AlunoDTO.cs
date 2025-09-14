using System.ComponentModel.DataAnnotations;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.DTOs
{
    public class AlunoDTO
    {
        public decimal? IdAluno { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "O Nome precisa ter no mínimo 3 caracteres")]
        public string NomeCompleto { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos")] public string CPF { get; set; }
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }

    public class AlunoRequest : PaginacaoRequest
    {
        public string Nome { get; set; }
    }
}
