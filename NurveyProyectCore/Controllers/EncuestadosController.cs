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
    public class EncuestadosController : Controller
    {

        private EncuestadosRepository encuestadosRepository;

        public EncuestadosController()
        {
            this.encuestadosRepository = new EncuestadosRepository();
        }

        [HttpGet]
        public Encuestado[] Get()
        {
            return encuestadosRepository.getAllEncuestados();
        }

        [HttpPost]
        public IActionResult Post(Encuestado encuestado)
        {
            this.encuestadosRepository.SaveEncuestados(encuestado);

            //var response = Request.CreateResponse<Encuestado>(System.Net.HttpStatusCode.Created, encuestado);

            return Ok(encuestado);
        }
    }
}