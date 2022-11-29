

namespace SistemaGestion.Models
{
    public class Producto
    {
        public int id { get; set; }

        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int stock { get; set; }

        public Producto()
        {
            id = 0;
            Descripcion = "";
            Costo = 0;
            PrecioVenta = 0;
            stock = 0;
        }
    }
    
}
