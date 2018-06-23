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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio EncuestadoRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class EncuestadosController : Controller
    {

        private EncuestadosRepository encuestadosRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public EncuestadosController()
        {
            this.encuestadosRepository = new EncuestadosRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todos los encuestado al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de encuestados.
        /// </summary>
        /// <returns>Lista de objetos Encuestados. El objeto encuestado posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion:</returns>
        [HttpGet]
        public Encuestado[] Get()
        {
            return encuestadosRepository.getAllEncuestados();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar un encuestado al repositorio.
        /// Recibe por parámetro un objeto encuestado.
        /// </summary>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion</param>
        [HttpPost]
        public IActionResult Post(Encuestado encuestado)
        {
            this.encuestadosRepository.SaveEncuestados(encuestado);
            //var response = Request.CreateResponse<Encuestado>(System.Net.HttpStatusCode.Created, encuestado);
            return Ok(encuestado);
        }
    }
}