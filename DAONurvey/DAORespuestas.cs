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
    /// Esta clase contiene los métodos de acceso a los datos relacionados con las Respuestas.
    /// Cuenta con los métodos de: Insertar y consultar una Respuesta.
    /// </summary>
    public class DAORespuestas
    {
        /// <summary>
        /// Este método obtiene todas las respuestas almacenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de todas las respuestas.
        /// </summary>
        /// <returns>Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</returns>
        public static Respuestas[] ObtenerRespuestas()
        {
            List<Respuestas> listaRespuestas = new List<Respuestas>();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * from Respuestas";
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Respuestas respuestas = new Respuestas();
                    respuestas.idRespuesta = int.Parse(dr["idRespuesta"].ToString());
                    respuestas.idPregunta = int.Parse(dr["idPregunta"].ToString());
                    respuestas.idEncuesta = int.Parse(dr["idEncuesta"].ToString());
                    respuestas.idEncuestado = int.Parse(dr["idEncuestado"].ToString());
                    respuestas.descripcionRespuesta = dr["descripcionRespuesta"].ToString();
                    respuestas.fechaRespuesta = DAOMetodosUtiles.ParsearFecha((dr["fechaRespuesta"].ToString()));

                    listaRespuestas.Add(respuestas);
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

            return listaRespuestas.ToArray<Respuestas>();
        }

        /// <summary>
        /// Este método inserta una respuesta y el encuestado al que corresponde esa respuesta en la base de datos.
        /// Recibe una lista de respuestas y un encuestado.
        /// </summary>
        /// <param name="listaRespuestas">Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</param>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: (idEncuestado, tiempoRespuesta, ubicacion)</param>
        public static void InsertarRespuestas(List<Respuestas> listaRespuestas, Encuestado encuestado)
        {
            SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
            cn.Open();
            SqlTransaction transaction;
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cn;
            transaction = cn.BeginTransaction();
            cmd.Transaction = transaction;

            try
            {

                int idEncuestado = 0;
                int idRespuesta = 1;
                bool primeraVez = true;

                foreach (var respuesta in listaRespuestas)
                {
                    if (primeraVez)
                    {
                        cmd.CommandText = @"Insert into Encuestados (tiempoRespuesta, ubicacion) values
                                        (@tiempoRespuesta, @ubicacion) SELECT SCOPE_IDENTITY();";

                        cmd.Parameters.AddWithValue("@tiempoRespuesta", encuestado.tiempoRespuesta);
                        cmd.Parameters.AddWithValue("@ubicacion", encuestado.ubicacion);

                        idEncuestado = int.Parse(cmd.ExecuteScalar().ToString());
                        primeraVez = false;
                    }

                    Pregunta pregunta = DAOPreguntas.ObtenerPreguntaPorCodigoYEncuesta(respuesta.idEncuesta, respuesta.codigoPregunta);


                    cmd.CommandText = @"Insert into Respuestas (idRespuesta, idPregunta, idEncuesta, idEncuestado, descripcionRespuesta, fechaRespuesta) values
                                                    (@idRespuesta, @idPregunta, @idEncuesta, @idEncuestado, @descripcionRespuesta, @fechaRespuesta)";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idRespuesta", idRespuesta);
                    cmd.Parameters.AddWithValue("@idPregunta", pregunta.idPregunta);
                    cmd.Parameters.AddWithValue("@idEncuesta", respuesta.idEncuesta);
                    cmd.Parameters.AddWithValue("@idEncuestado", idEncuestado);
                    cmd.Parameters.AddWithValue("@descripcionRespuesta", respuesta.descripcionRespuesta);
                    cmd.Parameters.AddWithValue("@fechaRespuesta", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    idRespuesta = idRespuesta + 1;

                    Encuesta encuesta = DAOEncuesta.ObtenerEncuesta(respuesta.idEncuesta);

                    if (encuesta.estadoEncuesta == "creada")
                    {
                        try
                        {
                            SqlConnection cn2 = new SqlConnection(CadenaConexion.cadenaConexion);
                            cn2.Open();
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.Connection = cn2;
                            cmd2.CommandText = @"Update Encuestas set estadoEncuesta='respondida' where idEncuesta=@idEncuesta";
                            cmd2.Parameters.AddWithValue("@idEncuesta", respuesta.idEncuesta);
                            cmd2.ExecuteNonQuery();
                            cn2.Close();
                        }

                        catch (Exception ex)
                        {
                            string mensaje = string.Empty;
                            mensaje = ex.ToString();
                            Console.WriteLine(mensaje);
                        }
                    }

                }

                transaction.Commit();
                cn.Close();
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }
    }
}
