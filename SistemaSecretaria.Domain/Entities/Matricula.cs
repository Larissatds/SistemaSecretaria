using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SistemaSecretaria.Domain.Entities
{
    [Table("TB_MATRICULAS")]
    public class Matricula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_MATRICULA")]
        public decimal IdMatricula { get; set; }

        [Required]
        [Column("ID_ALUNO")]
        public decimal IdAluno { get; set; }

        [Required]
        [Column("ID_TURMA")]
        public decimal IdTurma { get; set; }

        [Required]
        [Column("DATA_MATRICULA", TypeName = "date")]
        public DateTime DataMatricula { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O Status deve ter no máximo 100 caracteres")]
        [Column("STATUS")]
        public string Status { get; set; }

        public Aluno Aluno { get; set; } = null!;

        public Turma Turma { get; set; } = null!;
    }
}
