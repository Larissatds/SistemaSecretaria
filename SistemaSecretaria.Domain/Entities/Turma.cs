using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSecretaria.Domain.Entities
{
    [Table("TB_TURMAS")]
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_TURMA")]
        public decimal IdTurma { get; set; }

        [Required]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "O Nome da turma deve ter no máximo 256 caracteres")]
        [Column("NOME")]
        public string Nome { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }
    }
}
