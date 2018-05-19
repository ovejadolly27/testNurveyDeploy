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
    public class TipoPreguntaController : Controller
    {
        private TipoPreguntaRepository tiposPreguntasRepository;

        public TipoPreguntaController()
        {
            this.tiposPreguntasRepository = new TipoPreguntaRepository();
        }

        [HttpGet]
        public TipoPregunta[] Get()
        {
            return tiposPreguntasRepository.GetAllTiposPreguntas();
        }

        [HttpPost]
        public IActionResult Post(TipoPregunta tipoPregunta)
        {
            this.tiposPreguntasRepository.SaveTipoPregunta(tipoPregunta);

            //var response = Request.CreateResponse<TipoPregunta>(System.Net.HttpStatusCode.Created, tipoPregunta);

            return Ok(tipoPregunta);
        }

        [HttpDelete]
        public TipoPregunta DeleteTipoPRegunta(int idTipoPregunta)
        {
            TipoPregunta tipoPregunta = this.tiposPreguntasRepository.ObtenerTipoPregunta(idTipoPregunta);
            this.tiposPreguntasRepository.EliminarTipoPregunta(tipoPregunta.idTipoPregunta);
            return tipoPregunta;
        }

        [HttpPut]
        public IActionResult Put(TipoPregunta tipoPregunta)
        {
            this.tiposPreguntasRepository.ActualizarTipoPregunta(tipoPregunta);

            //var response = Request.CreateResponse<TipoPregunta>(System.Net.HttpStatusCode.OK, tipoPregunta);

            return Ok(tipoPregunta);
        }

        [Route("idTipoPregunta/{idTipoPregunta}")]
        [HttpGet]
        public TipoPregunta GetTipoPregunta(int idTipoPregunta)
        {
            return tiposPreguntasRepository.ObtenerTipoPregunta(idTipoPregunta);
        }

    }
}