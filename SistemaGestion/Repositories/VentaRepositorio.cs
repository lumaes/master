using SistemaGestion.Models;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class VentaRepositorio
    {
         
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
        public List<Venta> ListarVenta()
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
