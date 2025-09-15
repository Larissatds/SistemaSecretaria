using System.ComponentModel.DataAnnotations;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.DTOs
{
    public class MatriculaDTO
    {
        public decimal? IdMatricula { get; set; }

        [Required]
        public decimal IdAluno { get; set; }

        [Required]
        public decimal IdTurma { get; set; }

        [Required]
        public DateTime DataMatricula { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O Status deve ter no máximo 100 caracteres")]
        public string Status { get; set; }

        public Turma? Turma { get; set; }

        public Aluno? Aluno { get; set; }
    }

    public class MatriculaRequest : PaginacaoRequest
    {
        public decimal? IdTurma { get; set; }
    }
}
