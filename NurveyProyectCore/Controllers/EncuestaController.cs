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
    public class EncuestaController : Controller
    {
        private EncuestaRepository encuestasRepository;

        public EncuestaController()
        {
            this.encuestasRepository = new EncuestaRepository();
        }

        [HttpPost]
        public IActionResult Post(Encuesta encuesta)
        {
            encuesta.definicionJSON = JsonConvert.SerializeObject(encuesta.definicion);
            int newId = this.encuestasRepository.SaveEncuesta(encuesta);
            encuesta.idEncuesta = newId;

            //var response = Request.CreateResponse<Encuesta>(System.Net.HttpStatusCode.Created, encuesta);

            return Ok(encuesta);
        }

        [HttpGet]
        public Encuesta[] Get()
        {
            return encuestasRepository.GetAllEncuestas();
        }

        [HttpDelete]
        public Encuesta DeleteEncuesta(int idEncuesta)
        {
            Encuesta encuesta = this.encuestasRepository.ObtenerEncuesta(idEncuesta);
            this.encuestasRepository.EliminarEncuesta(encuesta.idEncuesta);
            return encuesta;
        }

        [Route("idEncuesta/{idEncuesta}")]
        [HttpGet]
        public Encuesta Get(int idEncuesta)
        {
            return encuestasRepository.ObtenerEncuesta(idEncuesta);
        }

        [Route("idUsuario/{idUsuario}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorUsuario(int idUsuario)
        {
            return encuestasRepository.ObtenerEncuestasPorUsuario(idUsuario);
        }
        
        [HttpPut]
        public IActionResult ActualizarPublicado(bool publicado, int idEncuesta, int idUsuario)
        {
            this.encuestasRepository.ActualizarPublicado(publicado, idEncuesta, idUsuario);

            //var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, idEncuesta); //check

            return Ok(this.encuestasRepository.ObtenerEncuesta(idEncuesta));
        }

        [HttpPut]
        public IActionResult ActualizarEstadoEncuesta(string estadoEncuesta, int idEncuesta, int idUsuario)
        {

            this.encuestasRepository.ActualizarEstadoEncuesta(estadoEncuesta, idEncuesta, idUsuario);

            //var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, idEncuesta); //check

            return Ok(this.encuestasRepository.ObtenerEncuesta(idEncuesta));
        }

        [HttpPut]
        public IActionResult ActualizarEncuesta(Encuesta encuesta)
        {
            this.encuestasRepository.ActualizarEncuesta(encuesta);
            //var response = Request.CreateResponse<Encuesta>(System.Net.HttpStatusCode.OK, encuesta);
            return Ok(encuesta);
        }

        [Route("filtro/{filtro}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorTitulo(string filtro)
        {
            return encuestasRepository.ObtenerEncuestasPorTitulo(filtro);
        }

        [Route("publicado/{publicado}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorPublicado(bool publicado)
        {
            return encuestasRepository.ObtenerEncuestasPorPublicado(publicado);
        }

        [Route("publicadoYEstado/{publicado}/{estadoEncuesta}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorPublicadoYEstado(bool publicado, string estadoEncuesta)
        {
            return encuestasRepository.ObtenerEncuestasPorPublicadoYEstado(publicado, estadoEncuesta);
        }

        [Route("estadoEncuesta/{estadoEncuesta}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorEstadoEncuesta(string estadoEncuesta)
        {
            return encuestasRepository.ObtenerEncuestasPorEstadoEncuesta(estadoEncuesta);
        }
    }
    

}