namespace PapelitosAPI.Modelos
{
    public class Telefonos
    {
        public int Id { get; set; }
        public int entidad_id { get; set; }
        public string numero { get; set; }
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
        public string Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Usuario_Modificacion { get; set; }
        public string Estado { get; set; } = "A"; // A: Activo, E: Eliminado, I: Inactivo
    }
}
