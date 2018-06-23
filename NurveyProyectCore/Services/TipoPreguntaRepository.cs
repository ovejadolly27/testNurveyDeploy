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
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de un Tipo de Pregunta.
    /// Cuenta con el método de: Obtener, insertar, eliminar y actualizar un tipo de pregunta.
    /// </summary>
    public class TipoPreguntaRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de obtener todos los tipos de preguntas almecenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de tipos de pregunta.
        /// </summary>
        /// <returns>Lista de objetos Tipo Pregunta. El objeto categoría posee los siguientes atributos: idTipoPregunta, descripcionTipoPregunta, type:</returns>
        public TipoPregunta[] GetAllTiposPreguntas()
        {
            return DAOTipoPregunta.ObtenerTiposPreguntas();
        }

        /// <summary>
        /// Este método delega la responsabilidad de insertar un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro un objeto pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        public bool SaveTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                DAOTipoPregunta.InsertarTipoPregunta(tipoPregunta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener un tipo de pregunta almacenada en la base de datos.
        /// Recibe por parámetro el id del tipo de pregunta a consultar y devuelve el tipo de pregunta que se corresponde con ese id.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idTipoPregunta,descripcionTipoPregunta, type)</returns>
        public TipoPregunta ObtenerTipoPregunta(int id)
        {
            return DAOTipoPregunta.ObtenerTipoPregunta(id);
        }

        /// <summary>
        /// Este método delega la responsabilidad de eliminar un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro el id del tipo de pregunta a eliminar.
        /// </summary>
        /// <param name="idTipoPregunta">ID (int) del tipo de pregunta a eliminar. Por ejemplo: 27</param>
        public void EliminarTipoPregunta(int idTipoPregunta)
        {
            DAOTipoPregunta.EliminarTipoPregunta(idTipoPregunta);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar los datos de un tipo de pregunta en la base de datos. 
        /// Recibe por parámetro un objeto tipo pregunta.
        /// </summary>
        /// <param name="tipoPregunta">Objeto que posee los siguientes atributos: idTipoPregunta,descripcionTipoPregunta, type </param>
        public void ActualizarTipoPregunta(TipoPregunta tipoPregunta)
        {
            DAOTipoPregunta.ActualizarTipoPregunta(tipoPregunta);
        }
    }
}
