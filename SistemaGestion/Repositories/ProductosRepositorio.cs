using SistemaGestion.Models;
using System.Data.SqlClient;


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
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            if(conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Producto", conexion))
                {
                    conexion.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Producto producto = new Producto();
                                producto.id = int.Parse(dr["Id"].ToString());
                                producto.Descripcion = dr["Descripciones"].ToString();
                                producto.PrecioVenta = double.Parse(dr["PrecioVenta"].ToString());
                                producto.Costo = double.Parse(dr["Costo"].ToString());
                                producto.stock = int.Parse(dr["Stock"].ToString());
                                lista.Add(producto);

                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch(Exception ex)
            {
                throw;
            }
            return lista;
        }
    }
}
