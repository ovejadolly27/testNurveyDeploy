using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

/// <summary>
/// Este proyecto contiene un conjunto de métodos que permiten delegar el comportamiento del acceso a los datos de la entidad correspondiente.
/// Se basa en el lenguaje C#.
/// </summary>
namespace NurveyProyectCore.Services
{
    /// <summary>
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de una Encuesta.
    /// Cuenta con los métodos de: Insertar, obtener, eliminar y actualizar una Encuesta.
    /// </summary>
    public class EncuestaRepository
    {

        /// <summary>
        /// Este método delega la responsabilidad de guardar una encuesta en la base de datos.
        /// Recibe por parámetro un objeto encuesta y devuelve el id de la Encuesta insertada en la base de datos.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</param>
        /// <returns>ID (int) de la encuesta insertada. Por ejemplo: 27</returns>
        public int SaveEncuesta(Encuesta encuesta)
        {
            try
            {
                return DAOEncuesta.InsertarEncuesta(encuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }

        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas almacenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de Encuestas.
        /// </summary>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta[] GetAllEncuestas()
        {
            return DAOEncuesta.ObtenerEncuestas();
        }

        /// <summary>
        /// Este método delega la responsabilidad de eliminar una encuesta almacenada en la base de datos.
        /// Recibe por parámetro el id de la encuesta a eliminar.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a eliminar. Por ejemplo: 27</param> 
        public void EliminarEncuesta(int idEncuesta)
        {
            DAOEncuesta.EliminarEncuesta(idEncuesta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener una encuesta almacenada en la base de datos.
        /// Recibe por parámetro el id de la encuesta a consultar y devuelve la encuesta que se corresponde con ese id.
        /// </summary>
        /// <param name="idEncuesta">ID (int) de la encuesta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta ObtenerEncuesta(int id)
        {
            return DAOEncuesta.ObtenerEncuesta(id);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas de un usuario almacenadas en la base de datos.
        /// Recibe por parámetro el id del usuario y devuelve una lista de encuestas de ese usuario.
        /// </summary>
        /// <param name="idUsuario">ID (int) del usuario para consultar sus encuestas creadas. Por ejemplo: 1</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta[] ObtenerEncuestasPorUsuario(int id)
        {
            return DAOEncuesta.ObtenerEncuestasPorUsuario(id);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar los datos de una encuesta almacenada en la base de datos.
        /// Recibe por parámetro una encuesta con los atributos a editar.
        /// Se actualizan los datos teniendo el cuenta el id de la encuesta recibida por parámetro.
        /// </summary>
        /// <param name="encuesta">Objeto que posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta) </param>
        public void ActualizarEncuesta(Encuesta encuesta)
        {
            DAOEncuesta.ActualizarEncuesta(encuesta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar el estado de una Encuesta. Los estados posibles pueden ser: (Creada, Respondida, Archivada).
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo estadoEncuesta para actualizar el estado de la encuesta.
        /// </summary>
        /// <param name="estadoEncuesta">String para actualizar el estado de la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de su encuesta. Por ejemplo: 1</param>
        public void ActualizarEstadoEncuesta(string estadoEncuesta, int idEncuesta, int idUsuario)
        {
            DAOEncuesta.ActualizarEstadoEncuesta(estadoEncuesta, idEncuesta, idUsuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar el estado de publicación (publicada o no publicada) de una encuesta.
        /// Recibe por parámetro el id de la encuesta, id del usuario y un atributo (publicado) para actualizar la estado de publicación de la encuesta.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para publicar y false para no publicar la encuesta</param>
        /// <param name="idEncuesta">ID (int) de la encuesta a editar su estado de publicación. Por ejemplo: 27</param>
        /// <param name="idUsuario">ID (int) del usuario para editar el estado de publicación de su encuesta. Por ejemplo: 1</param>
        public void ActualizarPublicado(bool publicado, int idEncuesta, int idUsuario)
        {
            DAOEncuesta.ActualizarPublicado(publicado, idEncuesta, idUsuario);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas filtradas por título de encuesta.
        /// Recibe por parámetro el filtro de título de encuesta y devuelve una lista de las encuestas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por título de encuesta. Por ejemplo: política</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>

        public Encuesta[] ObtenerEncuestasPorTitulo(string filtro)
        {
            return DAOEncuesta.ObtenerEncuestasPorTitulo(filtro);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas por estado de publicación.
        /// Recibe por parámetro el estado de publicación y devuelve una lista de encuestas filtradas por ese estado (publicada o no publicada).
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta[] ObtenerEncuestasPorPublicado(bool publicado)
        {
            return DAOEncuesta.ObtenerEncuestasPorPublicado(publicado);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas por estado de publicación y por estado de la encuesta.
        /// Recibe por parámetro el estado de publicación y el estado de encuesta, y devuelve una lista de encuestas filtradas por esos estados.
        /// </summary>
        /// <param name="publicado">Boolean con valor true para consultar encuestas publicadas y con valor false para consultar encuestas no publicadas</param>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta[] ObtenerEncuestasPorPublicadoYEstado(bool publicado, string estadoEncuesta)
        {
            return DAOEncuesta.ObtenerEncuestasPorPublicadoYEstado(publicado, estadoEncuesta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las encuestas por estado de Encuesta.
        /// Recibe por parámetro un estado y devuelve una lista de encuestas filtradas por ese estado.
        /// </summary>
        /// <param name="estadoEncuesta">String con un estado de encuesta para consultar. Los estados pueden ser: (creada, respondida, archivada)</param>
        /// <returns>Lista de objetos Encuestas. El objeto Encuesta posee los siguientes atributos: (idEncuesta, tituloEncuesta, definicion, idUsuario, publicado, estadoEncuesta, fechaEncuesta)</returns>
        public Encuesta[] ObtenerEncuestasPorEstadoEncuesta(string estadoEncuesta)
        {
            return DAOEncuesta.ObtenerEncuestasPorEstadoEncuesta(estadoEncuesta);
        }
    }
}
