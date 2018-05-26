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
    public class ResultadosController : Controller
    {
        private RespuestasRepository resultadosRepository;

        public ResultadosController()
        {
            this.resultadosRepository = new RespuestasRepository();
        }

        [Route("resultadosGraficos/{idEncuesta}/{idPregunta}")]
        [HttpGet]
        public ResultadosGraficos Get(int idEncuesta, int idPregunta)
        {
            return ResultadosRepository.ObtenerResultados(idEncuesta, idPregunta);
        }

        [Route("resultadosGraficos/{idUsuario}")]
        [HttpGet]
        public ResultadosGraficos Get(int idUsuario)
        {
            return ResultadosRepository.ObtenerResultadosEncuestasXUsuario(idUsuario);
        }
    }
}