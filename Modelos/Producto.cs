namespace PapelitosAPI.Modelos
{
    public class Producto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int id_categoria { get; set; }
        public string Usuario_Creacion { get; set; }
    }
}
