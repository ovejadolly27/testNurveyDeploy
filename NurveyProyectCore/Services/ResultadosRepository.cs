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
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de un Resultado.
    /// Cuenta con el método de: Obtener resultados.
    /// </summary>
    public class ResultadosRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de obtener los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id de la encuesta y el id de la pregunta a consultar y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 1</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>
        public static ResultadosGraficos ObtenerResultados(int idEncuesta, int idPregunta)
        {

            return DAOResultados.ObtenerDatos(idEncuesta, idPregunta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id del usuario y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idUsuario">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>
        internal static ResultadosGraficos ObtenerResultadosEncuestasXUsuario(int idUsuario)
        {
            return DAOResultados.ObtenerDatosEncuestasXUsuario(idUsuario);
        }
    }
}
