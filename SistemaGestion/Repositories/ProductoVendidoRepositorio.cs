using SistemaGestion.Models;
using System.Data;
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
                    foreach (ProductoVendido productoVendido in lista)
                    {
                        productoVendido.Producto = ProductosRepositorio.getSoloProductoPorId(productoVendido.IdProducto, conexion);
                    }
                    return lista;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            
        }
        public bool eliminarProductoVendido(int id)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("Delete From ProductoVendido WHERE Id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }
        public void CrearProductoVendido(ProductoVendido productoVendido)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES (@stock, @idProducto, @idVenta)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            conexion.Close();
        }
        public void editarProductoVendido(ProductoVendido productoVendido)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE ProductoVendido SET" +
                                                      "     Stock = @stock," +
                                                      "     IdProducto = @idProducto," +
                                                      "     IdVenta = @idVenta" +
                                                      "     WHERE Id = @id ", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = productoVendido.Id });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            conexion.Close();
        }
    }
}
