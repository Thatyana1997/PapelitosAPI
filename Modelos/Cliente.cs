using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelitosAPI.Modelos
{
    // Se conserva el nombre de la tabla asignada y la nulabilidad del script original.
    [Table("Cliente", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("nombre", TypeName = "nvarchar(100)")]
        public string Nombre { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        [Column("id_tipo_id")]
        public int? Id_Tipo_Id { get; set; }

        [StringLength(50)]
        [Column("identificacion", TypeName = "nvarchar(50)")]
        public string? Identificacion { get; set; }

        [Column("fecha_creacion", TypeName = "datetime")]
        public DateTime? Fecha_Creacion { get; set; }

        [StringLength(50)]
        [Column("usuario_creacion", TypeName = "nvarchar(50)")]
        public string? Usuario_Creacion { get; set; }

        [Column("fecha_modificacion", TypeName = "datetime")]
        public DateTime? Fecha_Modificacion { get; set; }

        [StringLength(50)]
        [Column("usuario_modificacion", TypeName = "nvarchar(50)")]
        public string? Usuario_Modificacion { get; set; }

        [RegularExpression("^[AEI]$", ErrorMessage = "El estado debe ser A, E o I.")]
        [Column("estado", TypeName = "char(1)")]
        public string? Estado { get; set; } = "A";
    }
}
