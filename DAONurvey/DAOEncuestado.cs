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
    /// Esta clase contiene los métodos de acceso a los datos relacionados con los Encuestados.
    /// Cuenta con los métodos de: Insertar y consultar Encuestados.
    /// </summary>
    public class DAOEncuestado
    {
        /// <summary>
        /// Este método obtiene todos los encuestados almacenados en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de encuestados.
        /// </summary>
        /// <returns>Lista de objetos Encuestados. El objeto encuestado posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion:</returns>
        public static Encuestado[] ObtenerEncuestados()
        {
            List<Encuestado> listaEncuestados = new List<Encuestado>();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * from Encuestados";
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Encuestado encuestado = new Encuestado();
                    encuestado.idEncuestado = int.Parse(dr["idEncuestado"].ToString());
                    encuestado.tiempoRespuesta = DateTime.Parse(dr["tiempoRespuesta"].ToString());
                    encuestado.ubicacion = dr["ubicacion"].ToString();
                    listaEncuestados.Add(encuestado);
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

            return listaEncuestados.ToArray<Encuestado>();
        }

        /// <summary>
        /// Este método inserta un encuestado en la base de datos. 
        /// Recibe por parámetro un objeto encuestado.
        /// </summary>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion</param>
        public static void InsertarEncuestado(Encuestado encuestado)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into Encuestados (tiempoRespuesta, ubicacion) values
                                                    (@tiempoRespuesta, @ubicacion)";
                cmd.Parameters.AddWithValue("@tiempoRespuesta", encuestado.tiempoRespuesta);
                cmd.Parameters.AddWithValue("@ubicacion", encuestado.ubicacion);
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
    }
}
