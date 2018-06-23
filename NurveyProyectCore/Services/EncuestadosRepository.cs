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
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de un Encuestado.
    /// Cuenta con los métodos de: Insertar y obtener un Encuestado.
    /// </summary>
    public class EncuestadosRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de obtener todas los encuestados almacenados en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de encuestados.
        /// </summary>
        /// <returns>Lista de objetos Encuestados. El objeto encuestado posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion:</returns>
        public Encuestado[] getAllEncuestados()
        {
            return DAOEncuestado.ObtenerEncuestados();
        }

        /// <summary>
        /// Este método delega la responsabilidad de guardar un encuestado en la base de datos.
        /// Recibe por parámetro un objeto encuestado.
        /// </summary>
        /// <param name="encuestado">Objeto que posee los siguientes atributos: idEncuestado, tiempoRespuesta, ubicacion</param>
        public bool SaveEncuestados(Encuestado encuestado)
        {
            try
            {
                DAOEncuestado.InsertarEncuestado(encuestado);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
