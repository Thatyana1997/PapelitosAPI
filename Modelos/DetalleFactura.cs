
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PapelitosAPI.Modelos
{

    public class DetalleFactura
    {
        
        public int Id { get; set; }

        public int id_factura { get; set; }

        public int id_producto { get; set; }

        public int cantidad { get; set; }


        public decimal precio_unitario { get; set; }


        public DateTime fecha_creacion { get; set; }


        public string usuario_creacion { get; set; }

        public DateTime? fecha_modificacion { get; set; }

        public string? usuario_modificacion { get; set; }

        public string estado { get; set; }
    }
}
