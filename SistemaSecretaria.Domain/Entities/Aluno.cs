using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaSecretaria.Domain.Entities
{
    [Table("TB_ALUNOS")]
    public class Aluno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_ALUNO")]
        public decimal IdAluno { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3, ErrorMessage = "O Nome precisa ter no mínimo 3 caracteres")]
        [Column("NOME_COMPLETO")]
        public string NomeCompleto { get; set; }

        [Required]
        [Column("DATA_NASCIMENTO", TypeName = "date")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos")]
        [Column("CPF")]
        public string CPF { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "O Email deve ter no máximo 256 caracteres")]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("SENHA_HASH")]
        public string SenhaHash { get; set; }

        [NotMapped]
        public List<Matricula> Matriculas { get; set; } = new List<Matricula>();

    }
}
