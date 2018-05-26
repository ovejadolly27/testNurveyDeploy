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
    public class CategoriaController : Controller
    {
        private CategoriaRepository categoriaRepository;

        public CategoriaController()
        {
            this.categoriaRepository = new CategoriaRepository();
        }

        [HttpGet]
        public Categoria[] Get()
        {
            return categoriaRepository.getAllCategorias();
        }

        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            this.categoriaRepository.SaveCategoria(categoria);

            //var response = Request.CreateResponse<Categoria>(System.Net.HttpStatusCode.Created, categoria);

            return Ok(categoria);
        }

        [Route("idCategoria/{idCategoria}")]
        [HttpGet]
        public Categoria Get(int idCategoria)
        {
            return categoriaRepository.ObtenerCategorias(idCategoria);
        }

        [HttpDelete]
        public Categoria DeleteCategoria(int idCategoria)
        {
            Categoria categoria = this.categoriaRepository.ObtenerCategorias(idCategoria);
            this.categoriaRepository.EliminarCategoria(categoria.idCategoria);
            return categoria;
        }

        [HttpPut]
        public IActionResult Put(Categoria categoria)
        {
            this.categoriaRepository.ActualizarCategoria(categoria);

            //var response = Request.CreateResponse<Categoria>(System.Net.HttpStatusCode.OK, categoria);

            return Ok(categoria);
        }
    }
}