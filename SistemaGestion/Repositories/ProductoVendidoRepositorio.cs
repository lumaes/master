using SistemaGestion.Models;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class ProductoVendidoRepositorio
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server= DESKTOP-CS3EA1C; database=SistemaGestion; Integrated Security = true;";

        public ProductoVendidoRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public List<ProductoVendido> ListarProductoVendido()
        {
            List<ProductoVendido> lista = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From ProductoVendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ProductoVendido productovendido = new ProductoVendido();
                                productovendido.Id = int.Parse(dr["Id"].ToString());
                                productovendido.Stock = int.Parse(dr["Stock"].ToString());
                                productovendido.IdProducto = int.Parse(dr["IdProducto"].ToString());
                                productovendido.IdVenta = int.Parse(dr["IdVenta"].ToString());
                                lista.Add(productovendido);

                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return lista;
        }
    }
}
