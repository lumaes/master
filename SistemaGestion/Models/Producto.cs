

namespace SistemaGestion.Models
{
    public class Producto
    {
        public int id { get; set; }

        public string? Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int stock { get; set; }
        public int IdUsuario { get; set; }

        public Producto(string descripciones, double precioVenta)
        {
            this.Descripciones = descripciones;
            this.PrecioVenta = precioVenta;
        }
        public Producto()
        {
            id = 0;
            Descripciones = "";
            Costo = 0;
            PrecioVenta = 0;
            stock = 0;
            IdUsuario = 0;
        }
    }
    
}
