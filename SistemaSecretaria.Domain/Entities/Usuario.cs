using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaSecretaria.Domain.Entities
{
    [Table("TB_USUARIOS")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_USUARIO")]
        public decimal IdUsuario { get; set; }

        [Column("NOME_COMPLETO")]
        public string NomeCompleto { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("SENHA_HASH")]
        public string SenhaHash { get; set; }

        [Column("ID_TIPO_USUARIO")]
        public decimal IdTipoUsuario { get; set; }

        [Column("DATA_CRIACAO")]
        public DateTime DataCriacao { get; set; }

        [Column("DATA_ULTIMO_LOGIN")]
        public DateTime? DataUltimoLogin { get; set; }

        public TipoUsuario TipoUsuario { get; set;}
    }
}
