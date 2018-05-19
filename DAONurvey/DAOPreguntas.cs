using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using EntidadesNurvey;




/// <summary>
/// Este proyecto contiene las clases que proporcionan el acceso a la base de datos.
/// Se basa en el lenguaje C#, utiliza SQL (Lenguaje de consulta estructurada) y sirve de interfaz de acceso hacia un servidor Microsoft SQL Server.
/// </summary>
namespace DAONurvey
{
    /// <summary>
    /// Esta clase contiene los métodos de acceso a los datos relacionados con las Preguntas.
    /// Cuenta con los métodos de: Insertar, modificar, consultar y eliminar una Pregunta.
    /// </summary>
    public class DAOPreguntas
    {
        /// <summary>
        /// Este método inserta una pregunta en la base de datos.
        /// Recibe por parámetro un objeto Pregunta y devuelve el id de la Pregunta insertada en la base de datos.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        public static void InsertarPregunta(Pregunta pregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                cmd.CommandText = @"Insert into Preguntas (idPregunta, descripcionPregunta, idTipoPregunta, idCategoria, name, idEncuesta, esAgrupable) values
                                                    (@idPregunta, @descripcionPregunta, @idTipoPregunta, @idCategoria, @name, @idEncuesta, @esAgrupable)";

                int idPregunta = obtenerUltimoIdPregunta(pregunta.idEncuesta);

                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                cmd.Parameters.AddWithValue("@descripcionPregunta", pregunta.descripcion);
                cmd.Parameters.AddWithValue("@idTipoPregunta", pregunta.idTipoPregunta);
                cmd.Parameters.AddWithValue("@idCategoria", pregunta.idCategoria);
                cmd.Parameters.AddWithValue("@name", pregunta.name);
                cmd.Parameters.AddWithValue("@idEncuesta", pregunta.idEncuesta);
                cmd.Parameters.AddWithValue("@esAgrupable", pregunta.esAgrupable);
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
        /// Este método obtiene el último número de id de pregunta almacenada en la base de datos.
        /// Recibe por parámetro el id de encuesta y devuelve el último número de id de pregunta de esa encuesta.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta para conocer su última pregunta almacenada. Por ejemplo: 27</param>
        /// <returns>ID (int) de la última pregunta almacenada. Por ejemplo: 33</returns>
        private static int obtenerUltimoIdPregunta(int idEncuesta)
        {
            int idPregunta = 0;

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select MAX(idPregunta) as id from Preguntas where idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["id"] == DBNull.Value)
                    {
                        idPregunta = 1;
                    }
                    else
                    {
                        idPregunta = int.Parse(dr["id"].ToString()) + 1;
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

            return idPregunta;
        }
        /// <summary>
        /// Este método actualiza los datos de una pregunta almacenada en la base de datos.
        /// Recibe por parámetro una pregunta con los atributos a editar.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        public static void ActualizarPregunta(Pregunta pregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Preguntas set descripcionPregunta=@descripcionPregunta, idTipoPregunta=@idTipoPregunta, idCategoria=@idCategoria where idPregunta=@idPregunta";
                cmd.Parameters.AddWithValue("@idPregunta", pregunta.idPregunta);
                cmd.Parameters.AddWithValue("@descripcionPregunta", pregunta.descripcion);
                cmd.Parameters.AddWithValue("@idTipoPregunta", pregunta.idTipoPregunta);
                cmd.Parameters.AddWithValue("@idCategoria", pregunta.idCategoria);
                cmd.Parameters.AddWithValue("@name", pregunta.name);
                cmd.Parameters.AddWithValue("@IdEncuesta", pregunta.idEncuesta);
                cmd.Parameters.AddWithValue("@esAgrupable", pregunta.esAgrupable);
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
        /// Este método elimina una pregunta alamacenada en la base de datos.
        /// Recibe por parámetro el id de la pregunta a eliminar.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a eliminar. Por ejemplo: 2</param>
        public static void EliminarPregunta(int idPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete from Preguntas where idPregunta=@idPregunta";
                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);
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
        /// Este método obtiene una pregunta almacenada en la base de datos.
        /// Recibe por parámetro el id de pregunta a consultar y devuelve la pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public static Pregunta ObtenerPregunta(int idPregunta)
        {
            Pregunta pregunta = new Pregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Preguntas p where p.idPregunta=@idPregunta";
                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    pregunta.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    pregunta.descripcion = dr["descripcionPregunta"].ToString();
                    pregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    pregunta.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    pregunta.name = dr["name"].ToString();
                    pregunta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    pregunta.esAgrupable = bool.Parse(dr["esAgrupable"].ToString());
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

            return pregunta;
        }

        /// <summary>
        /// Este método obtiene una pregunta específica de una encuesta.
        /// Recibe por parámetro el id de encuesta y el código de la pregunta a obtener de esa encuesta.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 27</param>
        /// <param name="codigoPregunta">String de la pregunta a consultar. Por ejemplo: pregunta1</param>
        /// <returns>Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public static Pregunta ObtenerPreguntaPorCodigoYEncuesta(int idEncuesta, string codigoPregunta)
        {
            Pregunta pregunta = new Pregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Preguntas p where p.idEncuesta=@idEncuesta AND name=@codigoPregunta";
                cmd.Parameters.AddWithValue("@codigoPregunta", codigoPregunta);
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    pregunta.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    pregunta.descripcion = dr["descripcionPregunta"].ToString();
                    pregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    pregunta.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    pregunta.name = dr["name"].ToString();
                    pregunta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    pregunta.esAgrupable = bool.Parse(dr["esAgrupable"].ToString());
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

            return pregunta;
        }

        /// <summary>
        /// Este método obtiene todas las preguntas filtradas por descripción de pregunta.
        /// Recibe por parámetro el filtro de descripcion de pregunta y devuelve una lista de las preguntas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por descripción de pregunta. Por ejemplo: Edad</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public static Pregunta[] ObtenerPreguntas(string filtro)
        {
            List<Pregunta> listaPreguntas = new List<Pregunta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                string consulta = "Select * From Preguntas ";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " WHERE (descripcionPregunta like @filtro)";
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }

                cmd.CommandText = consulta;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta();
                    pregunta.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    pregunta.descripcion = dr["descripcionPregunta"].ToString();
                    pregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    pregunta.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    pregunta.name = dr["name"].ToString();
                    pregunta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    pregunta.esAgrupable = bool.Parse(dr["esAgrupable"].ToString());

                    listaPreguntas.Add(pregunta);
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
            return listaPreguntas.ToArray<Pregunta>();
        }

        /// <summary>
        /// Este método obtiene todas las preguntas de una categoria determinada.
        /// Recibe el id de la categoria y devuelve una lista de de preguntas filtradas por esa categoria.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public static Pregunta[] ObtenerPreguntasCategoria(int idCategoria)
        {
            List<Pregunta> listaPregunta = new List<Pregunta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Preguntas where idCategoria=@idCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta();
                    pregunta.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    pregunta.descripcion = dr["descripcionPregunta"].ToString();
                    pregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    pregunta.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    pregunta.name = dr["name"].ToString();
                    pregunta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    pregunta.esAgrupable = bool.Parse(dr["esAgrupable"].ToString());

                    listaPregunta.Add(pregunta);
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

            return listaPregunta.ToArray<Pregunta>();
        }

        /// <summary>
        /// Este método obtiene todas las preguntas de un determinado tipo de pregunta.
        /// Recibe el id de tipo de pregunta y devuelve las preguntas filtradas por ese tipo.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public static Pregunta[] ObtenerPreguntasTipo(int idTipoPregunta)
        {
            List<Pregunta> listaPregunta = new List<Pregunta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Preguntas where idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", idTipoPregunta);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pregunta pregunta = new Pregunta();
                    pregunta.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    pregunta.descripcion = dr["descripcionPregunta"].ToString();
                    pregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    pregunta.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    pregunta.name = dr["name"].ToString();
                    pregunta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    pregunta.esAgrupable = bool.Parse(dr["esAgrupable"].ToString());

                    listaPregunta.Add(pregunta);

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

            return listaPregunta.ToArray<Pregunta>();
        }
    }
}
