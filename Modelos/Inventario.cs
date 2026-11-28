using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelitosAPI.Modelos;

[Table("Inventario", Schema = "dbo")]
public class Inventario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_producto")]
    public int? IdProducto { get; set; }

    [Column("cantidad_existencia")]
    public int? CantidadExistencia { get; set; }

    [Column("fecha_creacion")]
    public DateTime? FechaCreacion { get; set; }

    [Column("usuario_creacion")]
    public string? UsuarioCreacion { get; set; }

    [Column("fecha_modificacion")]
    public DateTime? FechaModificacion { get; set; }

    [Column("usuario_modificacion")]
    public string? UsuarioModificacion { get; set; }

    [Column("estado")]
    public string? Estado { get; set; }
}

public class InventarioSolicitud
{
    [Required, Range(1, int.MaxValue)]
    public int? IdProducto { get; set; }

    [Required, Range(0, int.MaxValue)]
    public int? CantidadExistencia { get; set; }

    [Required, StringLength(50)]
    public string Usuario { get; set; } = string.Empty;
}
