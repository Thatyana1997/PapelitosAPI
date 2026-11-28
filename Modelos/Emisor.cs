namespace PapelitosAPI.Modelos
{
    public class Emisor
    {
        public int id { get; set; }
        public string razon_social { get; set; }
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
        public string Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Usuario_Modificacion { get; set; }
        public string Estado { get; set; } = "A"; // A: Activo, E: Eliminado, I: Inactivo
    }
}
