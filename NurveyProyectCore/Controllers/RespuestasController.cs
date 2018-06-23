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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio RespuestasRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class RespuestasController : Controller
    {
        private RespuestasRepository respuestasRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public RespuestasController()
        {
            this.respuestasRepository = new RespuestasRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las respuestas al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de todas las respuestas.
        /// </summary>
        /// <returns>Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</returns>
        [HttpGet]
        public Respuestas[] Get()
        {
            return respuestasRepository.getAllRespuestas();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar una respuesta al repositorio.
        /// Recibe una lista de respuestas y un encuestado.
        /// </summary>
        /// <param name="listaRespuestas">Lista de respuestas. Cada objeto respuesta posee los siguientes atributos: idRespuesta, idPregunta, idEncuesta, idEncuestado, codigoPregunta, descripcionRespuesta</param>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: (idEncuestado, tiempoRespuesta, ubicacion)</param>
        [HttpPost]
        public IActionResult Post(RespuestaEncuestado respuestaEncuestado)
        {
            bool guardado = this.respuestasRepository.SaveRespuesta(respuestaEncuestado);

            string respuesta = "";

            //var responseJSON = "";
            //HttpResponseMessage response;

            if (guardado)
            {
                //responseJSON = "{\"status\":\"OK\",\"message\":\"Encuesta contestada exitosamente\"";
                //response = Request.CreateResponse(System.Net.HttpStatusCode.Created, responseJSON);

                respuesta = "Encuesta contestada exitosamente";
            }
            else
            {
                //responseJSON = "{\"status\":\"ERROR\",\"message\":\"No se guardo la encuesta\"";
                //response = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, responseJSON);

                respuesta = "No se guardó la encuesta";
            }

            return Ok(respuesta);
        }
    }
}