namespace PapelitosAPI.Modelos
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Emisor { get; set; }
        public DateTime? Fecha_Emision { get; set; }
        public decimal? Total { get; set; }
        public DateTime? Fecha_Creacion { get; set; } = DateTime.Now;
        public string? Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Usuario_Modificacion { get; set; }
        public string Estado { get; set; } = "A"; // A: Activo, E: Eliminado, I: Inactivo
    }
}
