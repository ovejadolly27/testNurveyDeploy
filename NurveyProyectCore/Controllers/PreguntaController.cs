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
    public class PreguntaController : Controller
    {
        private PreguntaRepository PreguntasRepository;

        public PreguntaController()
        {
            this.PreguntasRepository = new PreguntaRepository();
        }

        [HttpPost]
        public IActionResult Post(Pregunta pregunta)
        {
            this.PreguntasRepository.SavePregunta(pregunta);

            //var response = Request.CreateResponse<Pregunta>(System.Net.HttpStatusCode.Created, pregunta);

            return Ok(pregunta);
        }

        [HttpDelete]
        public Pregunta DeletePregunta(int idPregunta)
        {
            Pregunta pregunta = this.PreguntasRepository.ObtenerPregunta(idPregunta);
            this.PreguntasRepository.EliminarPregunta(pregunta.idPregunta);
            return pregunta;
        }

        [HttpPut]
        public IActionResult Put(Pregunta pregunta)
        {
            this.PreguntasRepository.ActualizarPregunta(pregunta);

            //var response = Request.CreateResponse<Pregunta>(System.Net.HttpStatusCode.OK, pregunta);

            return Ok(pregunta);
        }

        [Route("filtro/{filtro}")]
        [HttpGet]
        public Pregunta[] Get(string filtro)
        {
            return PreguntasRepository.GetAllPreguntas(filtro);
        }

        [HttpGet]
        public Pregunta[] Get()
        {
            return PreguntasRepository.GetAllPreguntas(null);
        }

        [Route("idPregunta/{idPregunta}")]
        [HttpGet]
        public Pregunta Get(int idPregunta)
        {
            return PreguntasRepository.ObtenerPregunta(idPregunta);
        }

        [Route("idCategoria/{idCategoria}")]
        [HttpGet]
        public Pregunta[] GetPreguntasCategoria(int idCategoria)
        {
            return PreguntasRepository.GetPreguntasCategoria(idCategoria);
        }

        [Route("idTipoPregunta/{idTipoPregunta}")]
        [HttpGet]
        public Pregunta[] GetPreguntasTipo(int idTipoPregunta)
        {
            return PreguntasRepository.GetPreguntasTipo(idTipoPregunta);
        }
    }
}