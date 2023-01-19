using SistemaGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class VentaRepositorio
    {
         // CONEXION CON BASE DE DATOS
        private SqlConnection conexion;
        private String cadenaConexion = "Server = DESKTOP-CS3EA1C ; database=SistemaGestion; Integrated Security = true;";

        public VentaRepositorio()
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
        //LISTAMOS TODAS LAS VENTAS DESDE SQL 
        public List<Venta> ListarVenta(int? id)
        {
            List<Venta> lista = new List<Venta>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = int.Parse(dr["Id"].ToString());
                                venta.Comentarios = dr["Comentarios"].ToString();
                                venta.idUsuario = int.Parse(dr["IdUsuario"].ToString());
                                lista.Add(venta);

                            }
                        }
                    }
                    foreach (Venta venta in lista)
                    {
                        venta.productoVendidos = getProductosVendidos((int)venta.Id);
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
        private List<ProductoVendido> getProductosVendidos(int idVenta)
        {
            try
            {
                List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
                string query = "SELECT A.Id, A.IdProducto, A.Stock, B.Descripciones, B.PrecioVenta " +
                    "FROM ProductoVendido AS A " +
                    "INNER JOIN Producto AS B " +
                    "ON A.IdProducto = B.Id " +
                    "WHERE A.IdVenta = @id";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = idVenta });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<Venta> lista = new List<Venta>();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido()
                                {
                                    Id = int.Parse(dr["Id"].ToString()),
                                    IdProducto = int.Parse(dr["IdProducto"].ToString()),
                                    Stock = int.Parse(dr["Stock"].ToString()),
                                    IdVenta = idVenta,
                                    Producto= new Producto()
                                    {
                                        Descripciones = dr["Descripciones"].ToString(),
                                        PrecioVenta = double.Parse(dr["PrecioVenta"].ToString())
                                    }

                                };
                                productosVendidos.Add(productoVendido);
                            }
                        }
                        return productosVendidos;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public bool eliminarVenta(int id)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("Delete From Venta WHERE Id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch 
            {
                throw;
            }
            return false;
        }
        public void CrearVenta(Venta venta)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) VALUES (@comentarios,@idUsuario)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.idUsuario });
                    cmd.ExecuteNonQuery();
                    if (venta.productoVendidos != null && venta.productoVendidos.Count > 0)
                    {
                        foreach (ProductoVendido productoVendido in venta.productoVendidos)
                        {
                            productoVendido.IdVenta = venta.Id;
                            registrarProductoVendido(productoVendido);
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        private ProductoVendido registrarProductoVendido(ProductoVendido productoVendido)
        {
                Producto? producto = ProductosRepositorio.getStockProductoPorId(productoVendido.IdProducto, conexion);
                if (producto != null)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) VALUES(@stock, @idProducto, @idVenta); SELECT @@Identity;", conexion))
                    {
                        cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.BigInt) { Value = productoVendido.Stock });
                        cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                        cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVendido.IdVenta });
                        productoVendido.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    editarStock(producto, productoVendido.Stock);
                }
                else
                {
                    throw new Exception("Producto no encontrado");
                }
                return productoVendido;
        }
        //EDITAMOS EL STOCK DEL PRODUCTO, SE DESCUENTA UNO EN CASO DE VENTA
        private void editarStock(Producto producto, int s)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Producto SET stock = @stock WHERE id = @id", conexion))
            {
                cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.stock - s });
                cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = producto.id });
                cmd.ExecuteNonQuery();
            }
        }
        public void editarVenta(Venta venta)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE Venta SET" +
                                                      "     Comentarios = @comentarios," +
                                                      "     IdUsuario = @idUsuario" +
                                                      "     WHERE Id = @id ", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = venta.Id });
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = venta.idUsuario });
                    cmd.ExecuteNonQuery();
                }

            }
            catch 
            {
                throw;
            }
            conexion.Close();
        }
    }
}
