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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio PreguntaRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class PreguntaController : Controller
    {
        private PreguntaRepository PreguntasRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public PreguntaController()
        {
            this.PreguntasRepository = new PreguntaRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar una pregunta al repositorio.
        /// Recibe por parámetro un objeto Pregunta.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        [HttpPost]
        public IActionResult Post(Pregunta pregunta)
        {
            this.PreguntasRepository.SavePregunta(pregunta);
            //var response = Request.CreateResponse<Pregunta>(System.Net.HttpStatusCode.Created, pregunta);
            return Ok(pregunta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita eliminar una pregunta al repositorio.
        /// Recibe por parámetro el id de la pregunta a eliminar.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a eliminar. Por ejemplo: 2</param>
        [HttpDelete]
        public Pregunta DeletePregunta(int idPregunta)
        {
            Pregunta pregunta = this.PreguntasRepository.ObtenerPregunta(idPregunta);
            this.PreguntasRepository.EliminarPregunta(pregunta.idPregunta);
            return pregunta;
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar los datos de una pregunta al repositorio.
        /// Recibe por parámetro una pregunta con los atributos a editar.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        [HttpPut]
        public IActionResult Put(Pregunta pregunta)
        {
            this.PreguntasRepository.ActualizarPregunta(pregunta);

            //var response = Request.CreateResponse<Pregunta>(System.Net.HttpStatusCode.OK, pregunta);

            return Ok(pregunta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas todas las preguntas filtradas por descripción de pregunta al repositorio.
        /// Recibe por parámetro el filtro de descripcion de pregunta y devuelve una lista de las preguntas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por descripción de pregunta. Por ejemplo: Edad</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        [Route("filtro/{filtro}")]
        [HttpGet]
        public Pregunta[] Get(string filtro)
        {
            return PreguntasRepository.GetAllPreguntas(filtro);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las preguntas al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de Encuestas.
        /// </summary>
        /// <returns>Lista de objetos Pregunta. El objeto Pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        [HttpGet]
        public Pregunta[] Get()
        {
            return PreguntasRepository.GetAllPreguntas(null);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener una pregunta al repositorio.
        /// Recibe por parámetro el id de pregunta a consultar y devuelve la pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        [Route("idPregunta/{idPregunta}")]
        [HttpGet]
        public Pregunta Get(int idPregunta)
        {
            return PreguntasRepository.ObtenerPregunta(idPregunta);
        }


        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las preguntas de una categoria determinada al repositorio.
        /// Recibe el id de la categoria y devuelve una lista de de preguntas filtradas por esa categoria.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        [Route("idCategoria/{idCategoria}")]
        [HttpGet]
        public Pregunta[] GetPreguntasCategoria(int idCategoria)
        {
            return PreguntasRepository.GetPreguntasCategoria(idCategoria);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las preguntas de un determinado tipo de pregunta.
        /// Recibe el id de tipo de pregunta y devuelve las preguntas filtradas por ese tipo.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        [Route("idTipoPregunta/{idTipoPregunta}")]
        [HttpGet]
        public Pregunta[] GetPreguntasTipo(int idTipoPregunta)
        {
            return PreguntasRepository.GetPreguntasTipo(idTipoPregunta);
        }
    }
}