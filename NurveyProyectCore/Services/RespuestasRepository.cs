using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

/// <summary>
/// Este proyecto contiene un conjunto de métodos que permiten delegar el comportamiento del acceso a los datos de la entidad correspondiente.
/// Se basa en el lenguaje C#.
/// </summary>
namespace NurveyProyectCore.Services
{
    /// <summary>
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de una Respuesta.
    /// Cuenta con los métodos de: Insertar y obtener una Respuesta.
    /// </summary>
    public class RespuestasRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de guardar una respuesta en la base de datos.
        /// Recibe una lista de respuestas y un encuestado.
        /// </summary>
        /// <param name="listaRespuestas">Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</param>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: (idEncuestado, tiempoRespuesta, ubicacion)</param>
        public bool SaveRespuesta(RespuestaEncuestado respuestaEncuestado)
        {
            List<Respuestas> listaRespuestas = respuestaEncuestado.listaRespuestas;
            Encuestado encuestado = respuestaEncuestado.encuestado;
            try
            {
                DAORespuestas.InsertarRespuestas(listaRespuestas, encuestado);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las respuestas almacenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de todas las respuestas.
        /// </summary>
        /// <returns>Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</returns>
        public Respuestas[] getAllRespuestas()
        {
            return DAORespuestas.ObtenerRespuestas();
        }
    }
}

