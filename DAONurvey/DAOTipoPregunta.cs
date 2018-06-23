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
    /// Esta clase contiene los métodos de acceso a los datos relacionados con los tipos de preguntas.
    /// Cuenta con los métodos de: Insertar, actualizar, eliminar y consultar tipos de preguntas.
    /// </summary>
    public class DAOTipoPregunta
    {
        /// <summary>
        /// Este método obtiene todos los tipos de preguntas almecenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de categorías.
        /// </summary>
        /// <returns>Lista de objetos Tipo Pregunta. El objeto categoría posee los siguientes atributos: idTipoPregunta, descripcionTipoPregunta, type:</returns>
        public static TipoPregunta[] ObtenerTiposPreguntas()
        {
            List<TipoPregunta> listaTipoPregunta = new List<TipoPregunta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas";
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TipoPregunta tipoPregunta = new TipoPregunta();
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
                    tipoPregunta.type = dr["type"].ToString();
                    listaTipoPregunta.Add(tipoPregunta);
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
            return listaTipoPregunta.ToArray<TipoPregunta>();
        }

        /// <summary>
        /// Este método inserta un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro un objeto pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        public static void InsertarTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into TipoPreguntas (descripcionTipoPregunta) values  (@descripcionTipoPregunta, @type)";
                cmd.Parameters.AddWithValue("@descripcionTipoPregunta", tipoPregunta.descripcion);
                cmd.Parameters.AddWithValue("@type", tipoPregunta.type);
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
        /// Este método actualiza un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro un objeto tipo pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        public static void ActualizarTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update TipoPreguntas set descripcionTipoPregunta=@descripcionTipoPregunta where idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", tipoPregunta.idTipoPregunta);
                cmd.Parameters.AddWithValue("@descripcionTipoPregunta", tipoPregunta.descripcion);
                cmd.Parameters.AddWithValue("@type", tipoPregunta.type);
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
        /// Este método elimina un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro el id del tipo de pregunta a eliminar.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a eliminar. Por ejemplo: 27</param>
        public static void EliminarTipoPregunta(int idTipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete from TipoPreguntas where idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", idTipoPregunta);
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
        /// Este método obtiene un tipo de pregunta almacenada en la base de datos.
        /// Recibe por parámetro el id del tipo de pregunta a consultar y devuelve el tipo de pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idTipoPregunta,descripcionTipoPregunta, type)</returns>
        public static TipoPregunta ObtenerTipoPregunta(int idTipoPregunta)
        {
            TipoPregunta tipoPregunta = new TipoPregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas t where t.idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", idTipoPregunta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
                    tipoPregunta.type = dr["type"].ToString();
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

            return tipoPregunta;
        }

        /// <summary>
        /// Este método obtiene el tipo de pregunta almacenada en la base de datos.
        /// Recibe por parámetro el tipo de pregunta a consultar y devuelve el tipo de pregunta que se corresponde con este dato.
        /// </summary>
        /// <param name="type">Type (string) del tipo de pregunta a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idTipoPregunta,descripcionTipoPregunta, type)</returns>
        public static TipoPregunta ObtenerTipoPregunta(string type)
        {
            TipoPregunta tipoPregunta = new TipoPregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas t where t.type=@type";
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.type = dr["type"].ToString();
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
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

            return tipoPregunta;
        }
    }
}
