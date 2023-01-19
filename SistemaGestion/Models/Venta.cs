namespace SistemaGestion.Models
{
    public class Venta
    {
       public int Id { get; set; }
        public string Comentarios { get; set; }

        public int idUsuario { get; set; }
        public List<ProductoVendido> productoVendidos { get; set; }

        public Venta()
        {

        }
        public Venta(int id, string comentarios, int idUsuario)
        {
            this.Id = id;
            this.Comentarios = comentarios;
            this.idUsuario = idUsuario;
            this.productoVendidos = new List<ProductoVendido>();
        }
    }
}
