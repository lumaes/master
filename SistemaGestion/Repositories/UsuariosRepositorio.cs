using SistemaGestion.Models;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class UsuariosRepositorio
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server = DESKTOP-CS3EA1C ; database=SistemaGestion; Integrated Security = true;";

        public UsuariosRepositorio()
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
        public List<Usuario> ListarUsuario()
        {
            List<Usuario> lista = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = int.Parse(dr["Id"].ToString());
                                usuario.Nombre = dr["Nombre"].ToString();
                                usuario.Apellido = dr["Apellido"].ToString();
                                usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                usuario.Contrasenia = dr["Contraseña"].ToString();
                                usuario.Mail = dr["Mail"].ToString();
                                lista.Add(usuario);

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
        public Usuario? getUsuarioNombreUsuario(string? nombreUsuario)
        {
            
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Usuario Where NombreUsuario = @nombreUsuario", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("@nombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });
                    //cmd.ExecuteNonQuery();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            
                                Usuario usuario = new Usuario();
                                usuario.Id = int.Parse(dr["Id"].ToString());
                                usuario.Nombre = dr["Nombre"].ToString();
                                usuario.Apellido = dr["Apellido"].ToString();
                                usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                                usuario.Contrasenia = dr["Contraseña"].ToString();
                                usuario.Mail = dr["Mail"].ToString();
                                return usuario;

                            
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
            finally
            {
                conexion.Close();
            }
        }
        public Usuario? getUsuarioId(int id)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("Select * From Usuario Where Id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.VarChar) { Value = id });
                    //cmd.ExecuteNonQuery();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            Usuario usuario = new Usuario();
                            usuario.Id = int.Parse(dr["Id"].ToString());
                            usuario.Nombre = dr["Nombre"].ToString();
                            usuario.Apellido = dr["Apellido"].ToString();
                            usuario.NombreUsuario = dr["NombreUsuario"].ToString();
                            usuario.Contrasenia = dr["Contraseña"].ToString();
                            usuario.Mail = dr["Mail"].ToString();
                            return usuario;


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
            finally
            {
                conexion.Close();
            }
        }
        public bool eliminarUsuario(int id)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("Delete From Usuario WHERE Id = @id", conexion))
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
        public void CrearUsuario(Usuario usuario)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES (@nombre, @apellido, @nombreUsuario ,@contrasenia ,@mail)", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            conexion.Close();
        }
        public Usuario? editarUsuario(int id, Usuario usuarioAeditar)
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                Usuario? usuario = this.getUsuarioId(id);
                if(usuario == null)
                {
                    return null;
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Usuario SET" +
                                                      "     Nombre = @nombre," +
                                                      "     Apellido = @apellido," +
                                                      "     NombreUsuario = @nombreUsuario," +
                                                      "     Contraseña = @contraseña," +
                                                      "     Mail = @mail" +
                                                      "     WHERE Id = @id ", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = usuario.Id });
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    cmd.ExecuteNonQuery();
                    return usuario;
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
        
        public bool login(string nombreUsuario, string contrasenia)
        {
            Usuario? usuario = this.getUsuarioNombreUsuario(nombreUsuario);
            if (contrasenia == null || usuario == null)
            {
                return false;
            }
            return usuario.Contrasenia == contrasenia;
        }
    }
}
