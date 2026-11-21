using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelitosAPI.Modelos
{
    public class Canton
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int? Id_Provincia { get; set; }

        [StringLength(100)]
        public string? Nombre { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Fecha_Creacion { get; set; }

        [StringLength(50)]
        public string? Usuario_Creacion { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? Fecha_Modificacion { get; set; }

        [StringLength(50)]
        public string? Usuario_Modificacion { get; set; }

        [Column(TypeName = "char(1)")]
        public string? Estado { get; set; } = "A";
    }
}
