using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaSecretaria.Domain.Entities
{
    [Table("TB_TIPOS_USUARIO")]
    public class TipoUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_TIPO_USUARIO")]
        public decimal IdTipoUsuario { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
