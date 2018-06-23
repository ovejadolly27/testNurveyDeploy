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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio TipoPreguntaRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class TipoPreguntaController : Controller
    {
        private TipoPreguntaRepository tiposPreguntasRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public TipoPreguntaController()
        {
            this.tiposPreguntasRepository = new TipoPreguntaRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todos los tipos de preguntas al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de categorías.
        /// </summary>
        /// <returns>Lista de objetos Tipo Pregunta. El objeto categoría posee los siguientes atributos: idTipoPregunta, descripcionTipoPregunta, type:</returns>
        [HttpGet]
        public TipoPregunta[] Get()
        {
            return tiposPreguntasRepository.GetAllTiposPreguntas();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar un tipo de pregunta al repositorio. 
        /// Recibe por parámetro un objeto pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        [HttpPost]
        public IActionResult Post(TipoPregunta tipoPregunta)
        {
            this.tiposPreguntasRepository.SaveTipoPregunta(tipoPregunta);

            //var response = Request.CreateResponse<TipoPregunta>(System.Net.HttpStatusCode.Created, tipoPregunta);

            return Ok(tipoPregunta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita eliminar un tipo de pregunta al repositorio.
        /// Recibe por parámetro el id del tipo de pregunta a eliminar.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a eliminar. Por ejemplo: 27</param>
        [HttpDelete]
        public TipoPregunta DeleteTipoPRegunta(int idTipoPregunta)
        {
            TipoPregunta tipoPregunta = this.tiposPreguntasRepository.ObtenerTipoPregunta(idTipoPregunta);
            this.tiposPreguntasRepository.EliminarTipoPregunta(tipoPregunta.idTipoPregunta);
            return tipoPregunta;
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar los datos de un tipo de pregunta al repositorio.
        /// Recibe por parámetro un objeto tipo pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        [HttpPut]
        public IActionResult Put(TipoPregunta tipoPregunta)
        {
            this.tiposPreguntasRepository.ActualizarTipoPregunta(tipoPregunta);
            //var response = Request.CreateResponse<TipoPregunta>(System.Net.HttpStatusCode.OK, tipoPregunta);
            return Ok(tipoPregunta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener un tipo de pregunta al repositorio.
        /// Recibe por parámetro el id del tipo de pregunta a consultar y devuelve el tipo de pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idTipoPregunta,descripcionTipoPregunta, type)</returns>
        [Route("idTipoPregunta/{idTipoPregunta}")]
        [HttpGet]
        public TipoPregunta GetTipoPregunta(int idTipoPregunta)
        {
            return tiposPreguntasRepository.ObtenerTipoPregunta(idTipoPregunta);
        }

    }
}