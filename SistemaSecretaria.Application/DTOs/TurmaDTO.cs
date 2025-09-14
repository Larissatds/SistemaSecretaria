using System.ComponentModel.DataAnnotations;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.DTOs
{
    public class TurmaDTO
    {
        public decimal? IdTurma { get; set; }

        [StringLength(256, MinimumLength = 3, ErrorMessage = "O Nome precisa ter no mínimo 3 caracteres")]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal? TotalAlunos { get; set; }

        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
