using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using Microsoft.AspNetCore.Mvc;
using NurveyProyectCore.Services;
using Newtonsoft.Json;

/// <summary>
/// Este proyecto contiene un conjunto de controladores que son utilizados para ser accedidos desde una vista. 
/// Se basa en el lenguaje C#.
/// </summary>
namespace NurveyProyectCore.Controllers
{
    /// <summary>
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio ResultadosRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class ResultadosController : Controller
    {
        private RespuestasRepository resultadosRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public ResultadosController()
        {
            this.resultadosRepository = new RespuestasRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todos los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id de la encuesta y el id de la pregunta a consultar y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 1</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>

        [Route("resultadosGraficos/{idEncuesta}/{idPregunta}")]
        [HttpGet]
        public ResultadosGraficos Get(int idEncuesta, int idPregunta)
        {
            return ResultadosRepository.ObtenerResultados(idEncuesta, idPregunta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todos los datos de los resultados de una pregunta de una encuesta.
        /// Recibe por parámetro el id del usuario y devuelve un objeto Resultados Graficos.
        /// </summary>
        /// <param name="idUsuario">ID (int) de la encuesta a consultar. Por ejemplo: 59</param>
        /// <returns>Objeto ResultadosGraficos que posee los siguientes atributos: (labels (lista de strings, series (lista de doubles))</returns>
        [Route("resultadosGraficos/{idUsuario}")]
        [HttpGet]
        public ResultadosGraficos Get(int idUsuario)
        {
            return ResultadosRepository.ObtenerResultadosEncuestasXUsuario(idUsuario);
        }
    }
}