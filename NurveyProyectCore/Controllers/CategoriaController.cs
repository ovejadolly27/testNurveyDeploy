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
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio CategoríaRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private CategoriaRepository categoriaRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public CategoriaController()
        {
            this.categoriaRepository = new CategoriaRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita todas las categorías al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de categorías.
        /// </summary>
        /// <returns>Lista de objetos Categoría. El objeto categoría posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria:</returns>
        [HttpGet]
        public Categoria[] Get()
        {
            return categoriaRepository.getAllCategorias();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar una categoría al repositorio.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria,nombreCategoria, descripcionCategoria </param>
        [HttpPost]
        public IActionResult Post([FromBody] Categoria categoria)
        {
            this.categoriaRepository.SaveCategoria(categoria);
            //var response = Request.CreateResponse<Categoria>(System.Net.HttpStatusCode.Created, categoria);
            return Ok(categoria);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener una categoría al repositorio.
        /// Recibe por parámetro el id de categoría a consultar y devuelve la categoría que se corresponde con ese id.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idCategoria, nombreCategoria, descripcionCategoria)</returns>
        [Route("idCategoria/{idCategoria}")]
        [HttpGet]
        public Categoria Get(int idCategoria)
        {
            return categoriaRepository.ObtenerCategorias(idCategoria);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita eliminar una categoría al repositorio. 
        /// Recibe por parámetro el id de la categoría a eliminar.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a eliminar. Por ejemplo: 27</param>
        [HttpDelete]
        public Categoria DeleteCategoria(int idCategoria)
        {
            Categoria categoria = this.categoriaRepository.ObtenerCategorias(idCategoria);
            this.categoriaRepository.EliminarCategoria(categoria.idCategoria);
            return categoria;
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar una categoría al repositorio. 
        /// Recibe por parámetro un objeto categoria.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria </param>
        [HttpPut]
        public IActionResult Put(Categoria categoria)
        {
            this.categoriaRepository.ActualizarCategoria(categoria);
            //var response = Request.CreateResponse<Categoria>(System.Net.HttpStatusCode.OK, categoria);
            return Ok(categoria);
        }
    }
}