using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesNurvey;
using System.Data.SqlClient;
using System.Configuration; 

/// <summary>
/// Este proyecto contiene las clases que proporcionan el acceso a la base de datos.
/// Se basa en el lenguaje C#, utiliza SQL (Lenguaje de consulta estructurada) y sirve de interfaz de acceso hacia un servidor Microsoft SQL Server.
/// </summary>
namespace DAONurvey
{
    /// <summary>
    /// Esta clase contiene los métodos de acceso a los datos relacionados con la Encuesta.
    /// Cuenta con los métodos de: Insertar, modificar, consultar y eliminar una Encuesta.
    /// </summary>
    public class DAOUsuario
    {
        /// <summary>
        /// Este método inserta un usuario en la base de datos.
        /// Recibe por parámetro un objeto usuario.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        public static void InsertarUsuario(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into Usuario (nombreUsuario, emailUsuario, passwordUsuario, fechaAlta) values
                                                    (@nombreUsuario, @emailUsuario, @passwordUsuario, @fechaAlta)";

                cmd.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
                cmd.Parameters.AddWithValue("@emailUsuario", usuario.emailUsuario);
                cmd.Parameters.AddWithValue("@passwordUsuario", usuario.passwordUsuario);
                cmd.Parameters.AddWithValue("@fechaAlta", usuario.fechaAlta);

                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        /// <summary>
        /// Este método obtiene todos los usuarios filtrados por nombre de usuario.
        /// Recibe por parámetro el filtro de nombre de usuario y devuelve una lista de usuarios flitrados.
        /// </summary>
        /// <param name="filtro">String para filtrar por nombre de usuario. Por ejemplo: Juan</param>
        /// <returns>Lista de objetos Usuario. El objeto Usuario posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public static Usuario[] ObtenerUsuarios(string filtro)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                string consulta = "Select u.idUsuario, u.nombreUsuario, u.emailUsuario, u.passwordUsuario,u.fechaAlta,max(e.fechaEncuesta) as ultimaEncuesta, count(*) as encuestasCreadas From Usuario u " +
                " left join Encuestas e ON u.idUsuario = e.idUsuario ";


                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " WHERE (u.nombreUsuario like @filtro OR u.emailUsuario like @filtro)";
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }
                consulta += " GROUP BY u.idUsuario, u.nombreUsuario, u.emailUsuario, u.passwordUsuario,u.fechaAlta ";

                cmd.CommandText = consulta;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    usuario.nombreUsuario = dr["nombreUsuario"].ToString();
                    usuario.emailUsuario = dr["emailUsuario"].ToString();
                    usuario.passwordUsuario = dr["passwordUsuario"].ToString();
                    usuario.fechaAlta = DAOMetodosUtiles.ParsearFecha(dr["fechaAlta"].ToString());

                    if (dr["ultimaEncuesta"] == DBNull.Value)
                    {
                        usuario.ultimaEncuesta = DateTime.MinValue;
                        usuario.encuestasCreadas = 0;
                    }
                    else
                    {
                        usuario.ultimaEncuesta = DAOMetodosUtiles.ParsearFecha(dr["ultimaEncuesta"].ToString());
                        usuario.encuestasCreadas = int.Parse(dr["encuestasCreadas"].ToString());
                    }

                    listaUsuarios.Add(usuario);
                }
                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return listaUsuarios.ToArray<Usuario>();
        }

        /// <summary>
        /// Este método actualiza los datos de un usuario almacenado en la base de datos.
        /// Recibe por parámetro un usuario con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id del usuario recibido por parámetro.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        public static void ActualizarUsuario(Usuario usuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Usuario set nombreUsuario=@nombreUsuario, emailUsuario=@emailUsuario, passwordUsuario=@passwordUsuario where idUsuario=@idUsuario";
                cmd.Parameters.AddWithValue("@nombreUsuario", usuario.nombreUsuario);
                cmd.Parameters.AddWithValue("@emailUsuario", usuario.emailUsuario);
                cmd.Parameters.AddWithValue("@passwordUsuario", usuario.passwordUsuario);
                cmd.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        /// <summary>
        /// Este método elimina un usuario almacenado en la base de datos.
        /// Recibe por parámetro el id del usuario a eliminar.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a eliminar. Por ejemplo: 27</param>
        public static void EliminarUsuario(int idUsuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete from Usuario where idUsuario=@idUsuario";
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.ExecuteNonQuery();
                cn.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        /// <summary>
        /// Este método obtiene un usuario almacenado en la base de datos.
        /// Recibe por parámetro el id del usuario a consultar y devuelve el usuario que se corresponde con ese id.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public static Usuario ObtenerUsuario(int idUsuario)
        {
            Usuario usuario = new Usuario();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select u.idUsuario, u.nombreUsuario, u.emailUsuario, u.passwordUsuario,u.fechaAlta," +
                                    "max(e.fechaEncuesta) as ultimaEncuesta, count(*) as encuestasCreadas " +
                                    "From Usuario u left join Encuestas e on u.idUsuario = e.idUsuario " +
                                    "where u.idUsuario = @idUsuario " +
                                    "GROUP BY u.idUsuario, u.nombreUsuario, u.emailUsuario, u.passwordUsuario,u.fechaAlta";
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    usuario.nombreUsuario = dr["nombreUsuario"].ToString();
                    usuario.emailUsuario = dr["emailUsuario"].ToString();
                    usuario.passwordUsuario = dr["passwordUsuario"].ToString();
                    usuario.fechaAlta = DAOMetodosUtiles.ParsearFecha(dr["fechaAlta"].ToString());

                    if (dr["ultimaEncuesta"] == DBNull.Value)
                    {
                        usuario.ultimaEncuesta = DateTime.MinValue;
                        usuario.encuestasCreadas = 0;
                    }
                    else
                    {
                        usuario.ultimaEncuesta = DAOMetodosUtiles.ParsearFecha(dr["ultimaEncuesta"].ToString());
                        usuario.encuestasCreadas = int.Parse(dr["encuestasCreadas"].ToString());
                    }

                }
                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return usuario;
        }


        /// <summary>
        /// Este método obtiene un usuario registrado almacenado en la base de datos.
        /// Recibe por parámetro el email y password del usuario y devuelve el usuario que se corresponde con esos datos.
        /// </summary>
        /// <param name="emailUsuario">Email (string) del usuario a obtener</param>
        /// <param name="passwordUsuario">Password (string) del usuario a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public static Usuario ObtenerUsuarioAutenticado(string emailUsuario, string passwordUsuario)
        {
            Usuario usuario = new Usuario();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Usuario u where u.emailUsuario=@emailUsuario and u.passwordUsuario=@passwordUsuario";
                cmd.Parameters.AddWithValue("@emailUsuario", emailUsuario);
                cmd.Parameters.AddWithValue("@passwordUsuario", passwordUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    usuario.nombreUsuario = dr["nombreUsuario"].ToString();
                    usuario.emailUsuario = dr["emailUsuario"].ToString();
                    usuario.passwordUsuario = dr["passwordUsuario"].ToString();
                }
                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return usuario;
        }

        /// <summary>
        /// Este método valida si el email ingresado en un registro de un usuario existe en la base de datos.
        /// Recibe por parámetro el email del usuario y en caso de existir ese email en la base de datos devuelve el usuario que se corresponde a ese email.
        /// </summary>
        /// <param name="emailUsuario">Email (string) del usuario a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public static Usuario ValidarEmailUsuario(string emailUsuario)
        {
            Usuario usuario = new Usuario();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Usuario u where u.emailUsuario=@emailUsuario";
                cmd.Parameters.AddWithValue("@emailUsuario", emailUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    usuario.nombreUsuario = dr["nombreUsuario"].ToString();
                    usuario.emailUsuario = dr["emailUsuario"].ToString();
                    usuario.passwordUsuario = dr["passwordUsuario"].ToString();
                }
                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return usuario;
        }
    }
}
