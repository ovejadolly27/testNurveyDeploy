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
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de una Pregunta.
    /// Cuenta con los métodos de: Insertar, obtener, eliminar y actualizar una Pregunta.
    /// </summary>
    public class PreguntaRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de guardar una pregunta en la base de datos.
        /// Recibe por parámetro un objeto Pregunta y devuelve el id de la Pregunta insertada en la base de datos.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        public bool SavePregunta(Pregunta pregunta)
        {
            try
            {
                DAOPreguntas.InsertarPregunta(pregunta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener una pregunta en la base de datos.
        /// Recibe por parámetro el id de pregunta a consultar y devuelve la pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public Pregunta ObtenerPregunta(int idPregunta)
        {
            return DAOPreguntas.ObtenerPregunta(idPregunta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de eliminar una pregunta en la base de datos.
        /// Recibe por parámetro el id de la pregunta a eliminar.
        /// </summary>
        /// <param name="idPregunta">ID (int) de la pregunta a eliminar. Por ejemplo: 2</param>
        public void EliminarPregunta(int idPregunta)
        {
            DAOPreguntas.EliminarPregunta(idPregunta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar los datos de una pregunta en la base de datos.
        /// Recibe por parámetro una pregunta con los atributos a editar.
        /// </summary>
        /// <param name="pregunta">Objeto que posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</param>
        public void ActualizarPregunta(Pregunta pregunta)
        {
            DAOPreguntas.ActualizarPregunta(pregunta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las preguntas filtradas por descripción de pregunta.
        /// Recibe por parámetro el filtro de descripcion de pregunta y devuelve una lista de las preguntas filtradas.
        /// </summary>
        /// <param name="filtro">String para filtrar por descripción de pregunta. Por ejemplo: Edad</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public Pregunta[] GetAllPreguntas(string filtro)
        {
            return DAOPreguntas.ObtenerPreguntas(filtro);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las preguntas almacenados en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de encuestados.
        /// </summary>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public Pregunta[] GetAllPreguntas()
        {
            return DAOPreguntas.ObtenerPreguntas(null);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las preguntas de una categoria determinada.
        /// Recibe el id de la categoria y devuelve una lista de de preguntas filtradas por esa categoria.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public Pregunta[] GetPreguntasCategoria(int idCategoria)
        {
            return DAOPreguntas.ObtenerPreguntasCategoria(idCategoria);
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las preguntas de un determinado tipo de pregunta.
        /// Recibe el id de tipo de pregunta y devuelve las preguntas filtradas por ese tipo.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 3</param>
        /// <returns>Lista de preguntas. El objeto pregunta posee los siguientes atributos: (idPregunta, name, descripcion, idTipoPregunta, idCategoria, idEncuesta)</returns>
        public Pregunta[] GetPreguntasTipo(int idTipoPregunta)
        {
            return DAOPreguntas.ObtenerPreguntasTipo(idTipoPregunta);
        }
    }
}
