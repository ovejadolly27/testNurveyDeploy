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
    /// Esta clase contiene los métodos de acceso a los datos relacionados con los Resultados.
    /// Cuenta con los métodos para generar los resultados de cada encuesta..
    /// </summary>
    public class DAOResultados
    {
        /// <summary>
        /// Este método obtiene la cantidad total de respuestas de una pregunta de una determinada encuesta.
        /// Recibe por parámetro el id de la encuesta y el id de la pregunta a consultar.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 1</param>
        /// <returns>Cantidad total de respuestas (int) de esa pregunta</returns>
        public static int ObtenerCantidadTotalDeRespuestas(int idEncuesta, int idPregunta)
        {
            int cantidadTotal = 0;
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT COUNT (*) as cantidad FROM Respuestas R, Preguntas P WHERE P.idPregunta = R.idPregunta AND P.idEncuesta=R.idEncuesta AND P.idEncuesta = @idEncuesta AND P.idPregunta = @idPregunta";
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);
                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cantidadTotal = int.Parse(dr["cantidad"].ToString());
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

            return cantidadTotal;
        }

        /// <summary>
        /// Este método obtiene los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id de la encuesta y el id de la pregunta a consultar y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 1</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>
        public static ResultadosGraficos ObtenerDatos(int idEncuesta, int idPregunta)
        {
            ResultadosGraficos rs = new ResultadosGraficos();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT R.idPregunta, R.descripcionRespuesta as descripcion, COUNT(*) as cantidad, MAX(r.fechaRespuesta) as ultimaRespuesta FROM Respuestas R, Preguntas P WHERE P.idPregunta = R.idPregunta AND P.idEncuesta=R.idEncuesta AND P.idPregunta = @idPregunta and p.IdEncuesta=@idEncuesta GROUP BY R.idPregunta, R.descripcionRespuesta";

                cmd.Parameters.AddWithValue("@idPregunta", idPregunta);
                cmd.Parameters.AddWithValue("@idEncuesta", idEncuesta);

                SqlDataReader dr = cmd.ExecuteReader();

                int cantidadTotal = ObtenerCantidadTotalDeRespuestas(idEncuesta, idPregunta);

                DateTime maximaFecha = DateTime.MinValue;

                while (dr.Read())
                {
                    rs.labels.Add(dr["descripcion"].ToString());
                    rs.series.Add(Math.Round((double.Parse(dr["cantidad"].ToString()) / cantidadTotal * 100), 2));
                    rs.cantidadTotalRespuestas = cantidadTotal;
                    DateTime d = DAOMetodosUtiles.ParsearFecha(dr["ultimaRespuesta"].ToString());
                    if (d.CompareTo(maximaFecha) > 0)
                    {
                        maximaFecha = d;
                    }
                }
                rs.ultimaActualizacion = maximaFecha;


                cn.Close();
                dr.Close();

            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return rs;
        }

        /// <summary>
        /// Este método obtiene los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id del usuario y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idUsuario">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>
        public static ResultadosGraficos ObtenerDatosEncuestasXUsuario(int idUsuario)
        {
            ResultadosGraficos rs = new ResultadosGraficos();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "SELECT YEAR(fechaEncuesta) as anio,MONTH(fechaEncuesta) as mes,count(*) as cantidad " +
                    " FROM Encuestas " +
                    " WHERE idUsuario = @idUsuario" +
                    " GROUP BY YEAR(fechaEncuesta),MONTH(fechaEncuesta)";

                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    rs.labels.Add(dr["anio"].ToString() + " - " + dr["mes"].ToString());
                    rs.series.Add(int.Parse(dr["cantidad"].ToString()));
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

            return rs;
        }
    }
}
