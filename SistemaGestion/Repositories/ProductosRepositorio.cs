using SistemaGestion.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;


namespace SistemaGestion.Repositories
{
    public class ProductosRepositorio
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server = DESKTOP-CS3EA1C ; database=SistemaGestion; Integrated Security = true;";

        public ProductosRepositorio()
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
        // LISTAMOS TODOS LOS PRODUCTOS DE LA BASE DE DATOS
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Producto", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Producto producto = GetProductoFromReader(dr);
                                lista.Add(producto);

                            }
                        }
                        return lista;
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
        // TRAEMOS LOS PRODUCTOS CREADOS POR EL USUARIO
        public List<Producto> getProductoPorId(int id)
        {
            List<Producto> productos = new List<Producto>();
            
                if (conexion == null)
                {
                    throw new Exception("Conection fallida.");
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario=@id", conexion))
                    {
                        conexion.Open();
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = GetProductoFromReader(reader);
                                productos.Add(producto); 
                            }
                        }
                        else
                        {
                            return null;
                        }
                        return productos;
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
        //
        public static Producto? getStockProductoPorId(int id, SqlConnection? conexion)
        {
            if (conexion == null)
            {
                throw new Exception("Conection failed.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Id, Stock FROM producto WHERE Id = @id", conexion))
                {
                    if (conexion.State == ConnectionState.Closed) conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Producto product = new Producto()
                            {
                                id = int.Parse(dr["Id"].ToString()),
                                stock = int.Parse(dr["Stock"].ToString())
                            };
                            return product;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public static Producto getSoloProductoPorId(int id, SqlConnection conexion)
        {
            if (conexion == null)
            {
                throw new Exception("Conection failed.");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Descripciones, PrecioVenta FROM producto WHERE Id = @id", conexion))
                {
                    if (conexion.State == ConnectionState.Closed) conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Producto producto = new Producto(dr["Descripciones"].ToString(), double.Parse(dr["PrecioVenta"].ToString()));
                            return producto;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public bool eliminarProducto(int id)
            {

                if (conexion == null)
                {
                    throw new Exception("Conexion no establecida");
                }
                try
                {
                    int filasAfectadas = 0;
                    using (SqlCommand cmd = new SqlCommand("Delete From Producto WHERE Id = @id", conexion))
                    {
                        conexion.Open();
                        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
                        filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
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
                //return false;
            }
            // CREAMOS UN PRODUCTO CON ID ALEATORIO CREADO POR LA BASE DE DATOS
            public void CrearProducto(Producto producto)
            {

                if (conexion == null)
                {
                    throw new Exception("Conexion no establecida");
                }
                try
                {

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES (@descripciones, @costo, @precioVenta ,@stock ,@idUsuario)", conexion))
                    {
                        conexion.Open();
                        cmd.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones });
                        cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo });
                        cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta });
                        cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.stock });
                        cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = producto.IdUsuario });
                        cmd.ExecuteNonQuery();
                    }

                }
                catch 
                {
                    throw;
                }
                conexion.Close();
            }
            public Producto editarProducto(int id, Producto producto)
            {

                if (conexion == null)
                {
                    throw new Exception("Conexion no establecida");
                }
                try
                {

                    using (SqlCommand cmd = new SqlCommand("UPDATE Producto SET" +
                                                          "     Descripciones = @descripciones," +
                                                          "     Costo = @costo," +
                                                          "     PrecioVenta = @precioVenta," +
                                                          "     Stock = @stock," +
                                                          "     IdUsuario = @idUsuario" +
                                                          "     WHERE Id = @id ", conexion))
                    {
                        conexion.Open();
                        cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                        cmd.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones });
                        cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo });
                        cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta });
                        cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.stock });
                        cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.Int) { Value = producto.IdUsuario });
                        cmd.ExecuteNonQuery();
                    return producto;
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
            //FUNCION PARA LEER LOS DATOS DEL PRODUCTO DESDE SQL
            private Producto GetProductoFromReader(SqlDataReader reader)
            {
                Producto producto = new Producto();
                producto.id = int.Parse(reader["Id"].ToString());
                producto.Descripciones = reader["Descripciones"].ToString();
                producto.Costo = double.Parse(reader["Costo"].ToString());
                producto.PrecioVenta = double.Parse(reader["PrecioVenta"].ToString());
                producto.stock = int.Parse(reader["Stock"].ToString());
                producto.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                return producto;
            }
        }

    }
