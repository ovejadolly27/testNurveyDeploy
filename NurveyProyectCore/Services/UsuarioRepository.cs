using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using DAONurvey;

/// <summary>
/// Este proyecto contiene un conjunto de métodos que permiten delegar el comportamiento del acceso a los datos de la entidad correspondiente.
/// Se basa en el lenguaje C#.
/// </summary>
namespace NurveyProyectCore.Services
{
    /// <summary>
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de un Usuario.
    /// Cuenta con el método de: Obtener, insertar, eliminar y actualizar un usuario.
    /// </summary>
    public class UsuarioRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de guardar un usuario en la base de datos.
        /// Recibe por parámetro un objeto usuario.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        public bool SaveUsuario(Usuario usuario)
        {
            try
            {
                usuario.fechaAlta = DateTime.Now;
                DAOUsuario.InsertarUsuario(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todos los usuarios filtrados por nombre de usuario.
        /// Recibe por parámetro el filtro de nombre de usuario y devuelve una lista de usuarios flitrados.
        /// </summary>
        /// <param name="filtro">String para filtrar por nombre de usuario. Por ejemplo: Juan</param>
        /// <returns>Lista de objetos Usuario. El objeto Usuario posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario[] GetAllUsuarios(string filtro)
        {
            return DAOUsuario.ObtenerUsuarios(filtro);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todos los usuarios almacenados en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de Usuarios.
        /// </summary>
        /// <returns>Lista de objetos Usuario. El objeto Usuario posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario[] GetAllUsuarios()
        {
            return DAOUsuario.ObtenerUsuarios(null);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener un usuario almacenado en la base de datos.
        /// Recibe por parámetro el id del usuario a consultar y devuelve el usuario que se corresponde con ese id.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario GetUsuario(int idUsuario)
        {
            return DAOUsuario.ObtenerUsuario(idUsuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de eliminar un usuario almacenado en la base de datos.
        /// Recibe por parámetro el id del usuario a eliminar.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a eliminar. Por ejemplo: 27</param>
        public void EliminarUsuario(int idUsuario)
        {
            DAOUsuario.EliminarUsuario(idUsuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener un usuario almacenado en la base de datos.
        /// Recibe por parámetro el id del usuario a consultar y devuelve el usuario que se corresponde con ese id.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario ObtenerUsuario(int id)
        {
            return DAOUsuario.ObtenerUsuario(id);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar los datos de un usuario almacenado en la base de datos.
        /// Recibe por parámetro un usuario con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id del usuario recibido por parámetro.
        /// </summary>
        /// <param name="usuario">Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</param>
        public void ActualizarUsuario(Usuario usuario)
        {
            DAOUsuario.ActualizarUsuario(usuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener un usuario registrado almacenado en la base de datos.
        /// Recibe por parámetro el email y password del usuario y devuelve el usuario que se corresponde con esos datos.
        /// </summary>
        /// <param name="emailUsuario">Email (string) del usuario a obtener</param>
        /// <param name="passwordUsuario">Password (string) del usuario a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario GetUsuarioAutenticado(string emailUsuario, string passwordUsuario)
        {
            return DAOUsuario.ObtenerUsuarioAutenticado(emailUsuario, passwordUsuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de validar si el email ingresado en un registro de un usuario existe en la base de datos.
        /// Recibe por parámetro el email del usuario y en caso de existir ese email en la base de datos devuelve el usuario que se corresponde a ese email.
        /// </summary>
        /// <param name="emailUsuario">Email (string) del usuario a obtener</param>
        /// <returns>Objeto que posee los siguientes atributos: (idUsuario, nombreUsuario, emailUsuario, passwordUsuario)</returns>
        public Usuario GetValidarEmailUsuario(string emailUsuario)
        {
            return DAOUsuario.ValidarEmailUsuario(emailUsuario);
        }
    }
}
