using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesNurvey;
using System.Data.SqlClient;

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
    public class DAOEncuesta
    {
        /// <summary>
        /// Este método inserta una encuesta en la base de datos. 
        /// Recibe por parámetro un objeto encuesta y devuelve el id de la Encuesta insertada en la base de datos.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</param>
        /// <returns>ID (int) de la encuesta insertada. Por ejemplo: 27</returns>
        public static int InsertarEncuesta(Encuesta encuesta)
        {
            SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
            Int32 idEncuesta = -1;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into Encuestas (tituloEncuesta,definicion,idUsuario,fechaEncuesta,publicado,estadoEncuesta) values
                                                    (@tituloEncuesta,@definicion,@idUsuario,@fechaEncuesta,@publicado,@estadoEncuesta); SELECT CAST(scope_identity() AS int)";
                cmd.Parameters.AddWithValue("@tituloEncuesta", encuesta.tituloEncuesta);
                cmd.Parameters.AddWithValue("@definicion", encuesta.definicionJSON);
                cmd.Parameters.AddWithValue("@idUsuario", encuesta.idUsuario);
                cmd.Parameters.AddWithValue("@publicado", false);
                cmd.Parameters.AddWithValue("@estadoEncuesta", "creada");
                //cmd.Parameters.AddWithValue("@idCategoriaEncuesta", encuesta.idCategoriaEncuesta);
                cmd.Parameters.AddWithValue("@fechaEncuesta", DateTime.Today);
                idEncuesta = (Int32)cmd.ExecuteScalar();

                if (idEncuesta > 0)
                {
                    //insertarDefinicionDesglozado(idEncuesta, encuesta.definicion);
                }
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
            finally
            {
                cn.Close();
            }
            return (int)idEncuesta;
        }

        /// <summary>
        /// Este método obtiene todas las encuestas almacenadas en la base de datos. 
        /// No recibe ningún parámetro y devuelve una lista de Encuestas.
        /// </summary>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestas()
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                string consulta = "Select * From Encuestas ";

                cmd.CommandText = consulta;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método elimina una encuesta almacenada en la base de datos.
        /// Recibe por parámetro el id de la encuesta a eliminar.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a eliminar. Por ejemplo: 27</param> 
        public static void EliminarEncuesta(int idEncuesta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete Encuestados from Encuestados E, Respuestas R where R.idEncuesta=@idEncuesta and E.idEncuestado=R.idEncuestado";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete Preguntas from Preguntas P where P.idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete Encuestas from Encuestas E where E.idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
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
        /// Este método obtiene una encuesta almacenada en la base de datos.
        /// Recibe por parámetro el id de la encuesta a consultar y devuelve la encuesta que se corresponde con ese id.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta ObtenerEncuesta(int idEncuesta)
        {
            Encuesta encuesta = new Encuesta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Encuestas e where e.idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());
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

            return encuesta;
        }

        /// <summary>
        /// Este método obtiene todas las encuestas de un usuario almacenadas en la base de datos.
        /// Recibe por parámetro el id del usuario y devuelve una lista de encuestas de ese usuario.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario para consultar sus encuestas creadas. Por ejemplo: 1</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestasPorUsuario(int idUsuario)
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Encuestas where idUsuario=@idUsuario ";
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método actualiza los datos de una encuesta almacenada en la base de datos.
        /// Recibe por parámetro una encuesta con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id de la encuesta recibida por parámetro.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta) </param>
        public static void ActualizarEncuesta(Encuesta encuesta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Encuestas set tituloEncuesta=@tituloEncuesta where idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", encuesta.idEncuesta);
                cmd.Parameters.AddWithValue("@tituloEncuesta", encuesta.tituloEncuesta);

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
        /// Este método actualiza el estado de publicación (publicada o no publicada) de una encuesta.
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo (publicado) para actualizar la estado de publicación de la encuesta.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para publicar y false para no publicar la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado de publicación. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de publicación de su encuesta. Por ejemplo: 1</param>
        public static void ActualizarPublicado(bool publicado, int idEncuesta, int idUsuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Encuestas set publicado=@publicado where idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                cmd.Parameters.AddWithValue("@publicado", publicado);

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
        /// Este método actualiza el estado de una Encuesta. Los estados posibles pueden ser: (Creada, Respondida, Archivada).
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo estadoEncuesta para actualizar el estado de la encuesta.
        /// </summary>
        /// <param name="estadoEncuesta">String para actualizar el estado de la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de su encuesta. Por ejemplo: 1</param>
        public static void ActualizarEstadoEncuesta(string estadoEncuesta, int idEncuesta, int idUsuario)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Encuestas set estadoEncuesta=@estadoEncuesta where idEncuesta=@idEncuesta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                cmd.Parameters.AddWithValue("@estadoEncuesta", estadoEncuesta);

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
        /// Este método obtiene todas las encuestas filtradas por título de encuesta.
        /// Recibe por parámetro el filtro de título de encuesta y devuelve una lista de las encuestas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por título de encuesta. Por ejemplo: política</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestasPorTitulo(string filtro)
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                string consulta = "Select * From Encuestas ";

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " WHERE (tituloEncuesta like @filtro)";
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }

                cmd.CommandText = consulta;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método obtiene todas las encuestas por estado de publicación.
        /// Recibe por parámetro el estado de publicación y devuelve una lista de encuestas filtradas por ese estado (publicada o no publicada).
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestasPorPublicado(bool publicado)
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Encuestas where publicado=@publicado";
                cmd.Parameters.AddWithValue("@publicado", publicado);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método obtiene todas las encuestas por estado de publicación y por estado de la encuesta.
        /// Recibe por parámetro el estado de publicación y el estado de encuesta, y devuelve una lista de encuestas filtradas por esos estados.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestasPorPublicadoYEstado(bool publicado, string estadoEncuesta)
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Encuestas where publicado=@publicado and estadoEncuesta=@estadoEncuesta";
                cmd.Parameters.AddWithValue("@estadoEncuesta", estadoEncuesta);
                cmd.Parameters.AddWithValue("@publicado", publicado);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método obtiene todas las encuestas por estado de Encuesta.
        /// Recibe por parámetro un estado y devuelve una lista de encuestas filtradas por ese estado.
        /// </summary>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public static Encuesta[] ObtenerEncuestasPorEstadoEncuesta(string estadoEncuesta)
        {
            List<Encuesta> listaEncuesta = new List<Encuesta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Encuestas where estadoEncuesta=@estadoEncuesta";
                cmd.Parameters.AddWithValue("@estadoEncuesta", estadoEncuesta);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuesta encuesta = new Encuesta();
                    encuesta.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    encuesta.tituloEncuesta = dr["tituloEncuesta"].ToString();
                    encuesta.definicionJSON = dr["definicion"].ToString();
                    encuesta.idUsuario = int.Parse(dr["idUsuario"].ToString());
                    encuesta.publicado = bool.Parse(dr["publicado"].ToString());
                    encuesta.estadoEncuesta = dr["estadoEncuesta"].ToString();
                    //encuesta.idCategoriaEncuesta = int.Parse(dr["idCategoriaEncuesta"].ToString());
                    encuesta.fechaEncuesta = DateTime.Parse(dr["fechaEncuesta"].ToString());

                    listaEncuesta.Add(encuesta);
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

            return listaEncuesta.ToArray<Encuesta>();
        }

        /// <summary>
        /// Este método inserta por cada encuesta creada, los diferentes elementos de esa encuesta. 
        /// Los elementos son todas las preguntas que componen esa encuesta.
        /// Recibe por parámetro el id de la Encuesta y una Encuesta de tipo EncuestaSurvey.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta insertada. Por ejemplo: 27</param>
        /// <param name="encuestaSurvey">Objeto que posee los siguentes atributos: (title, List PaginaSurvey). 
        /// La lista PaginaSurvey tiene ElementSurvey, los cuales poseen los siguientes atributos: (isRequired, name, title, choices, columns, rows, html)</param>
        public static void insertarDefinicionDesglozado(int idEncuesta, EncuestaSurvey encuestaSurvey)
        {
            foreach (PaginaSurvey p in encuestaSurvey.pages)
            {
                foreach (ElementSurvey e in p.elements)
                {
                    TipoPregunta tipo = DAOTipoPregunta.ObtenerTipoPregunta(e.type);
                    Pregunta pregunta = new Pregunta();

                    pregunta.name = e.name;
                    pregunta.descripcion = e.title;
                    pregunta.idCategoria = 1;
                    pregunta.idTipoPregunta = tipo.idTipoPregunta;
                    pregunta.idEncuesta = idEncuesta;

                    DAOPreguntas.InsertarPregunta(pregunta);
                }
            }
        }
    }
}
