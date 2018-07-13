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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio EncuestaRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class EncuestaController : Controller
    {

        private EncuestaRepository encuestasRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public EncuestaController()
        {
            this.encuestasRepository = new EncuestaRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar una encuesta al repositorio.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta) </param>
        [HttpPost]
        public IActionResult Post([FromBody] Encuesta encuesta)
        {
            encuesta.definicionJSON = JsonConvert.SerializeObject(encuesta.definicion);
            int newId = this.encuestasRepository.SaveEncuesta(encuesta);
            encuesta.idEncuesta = newId;
            //var response = Request.CreateResponse<Encuesta>(System.Net.HttpStatusCode.Created, encuesta);
            return Ok(encuesta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las encuesta al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de Encuestas.
        /// </summary>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [HttpGet]
        public Encuesta[] Get()
        {
            return encuestasRepository.GetAllEncuestas();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita eliminar una encuesta al repositorio. 
        /// Recibe por parámetro el id de la encuesta a eliminar.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a eliminar. Por ejemplo: 27</param> 
        [HttpDelete]
        public Encuesta DeleteEncuesta(int idEncuesta)
        {
            Encuesta encuesta = this.encuestasRepository.ObtenerEncuesta(idEncuesta);
            this.encuestasRepository.EliminarEncuesta(encuesta.idEncuesta);
            return encuesta;
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener una encuesta al repositorio.
        /// Recibe por parámetro el id de la encuesta a consultar y devuelve la encuesta que se corresponde con ese id.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("idEncuesta/{idEncuesta}")]
        [HttpGet]
        public Encuesta Get(int idEncuesta)
        {
            return encuestasRepository.ObtenerEncuesta(idEncuesta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener las encuestas de un usuario al repositorio.
        /// Recibe por parámetro el id del usuario y devuelve una lista de encuestas de ese usuario.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario para consultar sus encuestas creadas. Por ejemplo: 1</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("idUsuario/{idUsuario}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorUsuario(int idUsuario)
        {
            return encuestasRepository.ObtenerEncuestasPorUsuario(idUsuario);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar el estado de publicación (publicada o no publicada) de una encuesta al repositorio.
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo (publicado) para actualizar la estado de publicación de la encuesta.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para publicar y false para no publicar la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado de publicación. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de publicación de su encuesta. Por ejemplo: 1</param>
        [HttpPut]
        public IActionResult ActualizarPublicado(bool publicado, int idEncuesta, int idUsuario)
        {
            this.encuestasRepository.ActualizarPublicado(publicado, idEncuesta, idUsuario);
            //var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, idEncuesta); //check
            return Ok(this.encuestasRepository.ObtenerEncuesta(idEncuesta));
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar el estado de una encuesta al repositorio. Los estados posibles pueden ser: (Creada, Respondida, Archivada).
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo estadoEncuesta para actualizar el estado de la encuesta.
        /// </summary>
        /// <param name="estadoEncuesta">String para actualizar el estado de la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de su encuesta. Por ejemplo: 1</param>
        [HttpPut]
        public IActionResult ActualizarEstadoEncuesta(string estadoEncuesta, int idEncuesta, int idUsuario)
        {
            this.encuestasRepository.ActualizarEstadoEncuesta(estadoEncuesta, idEncuesta, idUsuario);
            //var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, idEncuesta); //check
            return Ok(this.encuestasRepository.ObtenerEncuesta(idEncuesta));
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar una encuesta al repositorio.
        /// Recibe por parámetro una encuesta con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id de la encuesta recibida por parámetro.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta) </param>
        [HttpPut]
        public IActionResult ActualizarEncuesta(Encuesta encuesta)
        {
            this.encuestasRepository.ActualizarEncuesta(encuesta);
            //var response = Request.CreateResponse<Encuesta>(System.Net.HttpStatusCode.OK, encuesta);
            return Ok(encuesta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las encuestas filtradas por título de encuesta.
        /// Recibe por parámetro el filtro de título de encuesta y devuelve una lista de las encuestas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por título de encuesta. Por ejemplo: política</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("filtro/{filtro}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorTitulo(string filtro)
        {
            return encuestasRepository.ObtenerEncuestasPorTitulo(filtro);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las encuestas por estado de publicación.
        /// Recibe por parámetro el estado de publicación y devuelve una lista de encuestas filtradas por ese estado (publicada o no publicada).
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("publicado/{publicado}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorPublicado(bool publicado)
        {
            return encuestasRepository.ObtenerEncuestasPorPublicado(publicado);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las encuestas por estado de publicación y por estado de la encuesta.
        /// Recibe por parámetro el estado de publicación y el estado de encuesta, y devuelve una lista de encuestas filtradas por esos estados.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("publicadoYEstado/{publicado}/{estadoEncuesta}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorPublicadoYEstado(bool publicado, string estadoEncuesta)
        {
            return encuestasRepository.ObtenerEncuestasPorPublicadoYEstado(publicado, estadoEncuesta);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas las encuestas por estado de Encuesta.
        /// Recibe por parámetro un estado y devuelve una lista de encuestas filtradas por ese estado.
        /// </summary>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        [Route("estadoEncuesta/{estadoEncuesta}")]
        [HttpGet]
        public Encuesta[] GetEncuestasPorEstadoEncuesta(string estadoEncuesta)
        {
            return encuestasRepository.ObtenerEncuestasPorEstadoEncuesta(estadoEncuesta);
        }
    }
    

}