using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using Microsoft.AspNetCore.Mvc;
using NurveyProyectCore.Services;

/// <summary>
/// Este proyecto contiene un conjunto de controladores que son utilizados para ser accedidos desde una vista. 
/// Se basa en el lenguaje C#.
/// </summary>
namespace NurveyProyectCore.Controllers
{
    /// <summary>
    /// Esta clase contiene un conjunto de Web APIs que reciben peticiones de la vista y delegan las acciones correspondientes al repositorio UsuarioRepository. 
    /// </summary>
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository UsuariosRepository;

        /// <summary>
        /// Este método inicializa el repositorio con el que se va a conectar el controlador.
        /// </summary>
        public UsuarioController()
        {
            this.UsuariosRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita guardar un usuario al repositorio. 
        /// Recibe por parámetro un objeto usuario.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            this.UsuariosRepository.SaveUsuario(usuario);
            //var response = Request.CreateResponse<Usuario>(System.Net.HttpStatusCode.Created, usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todos los usuarios filtrados por nombre de usuario al repositorio.
        /// Recibe por parámetro el filtro de nombre de usuario y devuelve una lista de usuarios flitrados.
        /// </summary>
        /// <param name="filtro">String para filtrar por nombre de usuario. Por ejemplo: Juan</param>
        /// <returns>Lista de objetos Usuario. El objeto Usuario posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        [Route("filtro/{filtro}")]
        [HttpGet]
        public Usuario[] Get(string filtro)
        {
           return UsuariosRepository.GetAllUsuarios(filtro);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener todas los usuarios al repositorio.
        /// No recibe ningún parámetro y devuelve una lista de Usuarios.
        /// </summary>
        /// <returns>Lista de objetos Usuario. El objeto Usuario posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        [HttpGet]
        public Usuario[] Get()
        {
            return UsuariosRepository.GetAllUsuarios(null);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener un usuario al repositorio.
        /// Recibe por parámetro el id del usuario a consultar y devuelve el usuario que se corresponde con ese id.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        [Route("idUsuario/{idUsuario}")]
        [HttpGet]
        public Usuario Get(int idUsuario)
        {
            return UsuariosRepository.GetUsuario(idUsuario);
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita obtener un usuario registrado al repositorio.
        /// Recibe por parámetro el email y password del usuario y devuelve el usuario que se corresponde con esos datos.
        /// </summary>
        /// <param name="emailUsuario">Email (string) del usuario a obtener</param>
        /// <param name="passwordUsuario">Password (string) del usuario a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
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

        /// <summary>
        /// Este método recibe una petición de la vista y solicita eliminar un usuario al repositorio.
        /// Recibe por parámetro el id del usuario a eliminar.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a eliminar. Por ejemplo: 27</param>
        [HttpDelete("{idUsuario}")]
        public Usuario DeleteUsuario(int idUsuario)
        {
            Usuario usuario = this.UsuariosRepository.ObtenerUsuario(idUsuario);
            this.UsuariosRepository.EliminarUsuario(usuario.idUsuario);
            return usuario;
        }

        /// <summary>
        /// Este método recibe una petición de la vista y solicita actualizar los datos de un usuario almacenado en la base de datos.
        /// Recibe por parámetro un usuario con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id del usuario recibido por parámetro.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        [HttpPut]
        public IActionResult Put([FromBody] Usuario usuario)
        {
            this.UsuariosRepository.ActualizarUsuario(usuario);
            return Ok(usuario);
        }
    }
}
