namespace SistemaGestion.Models
{
    public class ProductoVendido
    {
       public int Id { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }

        public Producto? Producto { get; set; }

        public ProductoVendido()
        {

        }

        public ProductoVendido(int id, int idProducto, int stock, int idVenta, Producto producto)
        {
            this.Id = id;
            this.Stock = stock;
            this.IdProducto = idProducto;
            this.IdVenta = idVenta;
            this.Producto = producto;
        }

    }
}
