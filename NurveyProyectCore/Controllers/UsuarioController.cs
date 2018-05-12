using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using Microsoft.AspNetCore.Mvc;
using NurveyProyectCore.Services;

namespace NurveyProyectCore.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository UsuariosRepository;

        public UsuarioController()
        {
            this.UsuariosRepository = new UsuarioRepository();
        }

        //[HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            this.UsuariosRepository.SaveUsuario(usuario);

            //var response = Request.CreateResponse<Usuario>(System.Net.HttpStatusCode.Created, usuario);

            return Ok(usuario);
        }
        [Route("filtro/{filtro}")]
        [HttpGet]
        public Usuario[] Get(string filtro)
        {
           return UsuariosRepository.GetAllUsuarios(filtro);
        }

        [HttpGet]
        public Usuario[] Get()
        {
            return UsuariosRepository.GetAllUsuarios(null);
        }

        [HttpGet("{idUsuario}")]
        //[Route("IdUsuario/{idUsuario}")]
        //[HttpGet]
        public Usuario Get(int idUsuario)
        {
            return UsuariosRepository.GetUsuario(idUsuario);
        }

        [Route("autenticacion/{emailUsuario}/{passwordUsuario}")]
        [HttpGet]
        public Usuario Get(string emailUsuario, string passwordUsuario)
        {
            if (passwordUsuario == null || passwordUsuario == "")
            {
                return UsuariosRepository.GetValidarEmailUsuario(emailUsuario);
            }

            else
            {
                return UsuariosRepository.GetUsuarioAutenticado(emailUsuario, passwordUsuario);
            }
        }
        //[HttpDelete("{idUsuario}")]
        public Usuario DeleteUsuario(int idUsuario)
        {
            Usuario usuario = this.UsuariosRepository.ObtenerUsuario(idUsuario);
            this.UsuariosRepository.EliminarUsuario(usuario.idUsuario);
            return usuario;
        }

        //[HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            this.UsuariosRepository.ActualizarUsuario(usuario);
            return Ok(usuario);
        }
    }
}
