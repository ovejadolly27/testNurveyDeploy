using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using Microsoft.AspNetCore.Mvc;
using NurveyProyectCore.Services;
using Newtonsoft.Json;

namespace NurveyProyectCore.Controllers
{
    [Route("api/[controller]")]
    public class RespuestasController : Controller
    {
        private RespuestasRepository respuestasRepository;

        public RespuestasController()
        {
            this.respuestasRepository = new RespuestasRepository();
        }

        [HttpGet]
        public Respuestas[] Get()
        {
            return respuestasRepository.getAllRespuestas();
        }

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