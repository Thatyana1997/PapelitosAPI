namespace PapelitosAPI.Modelos
{
    public class Parametro
    {
        public int Id { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
        public string Usuario_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string? Usuario_Modificacion { get; set; }
        public string Estado { get; set; } = "A";
    }
}
