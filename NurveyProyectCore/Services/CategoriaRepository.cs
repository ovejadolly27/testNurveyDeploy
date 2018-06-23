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
    /// Esta clase contiene un conjunto de servicios que delegan el comportamiento del acceso a los datos de una Categoría.
    /// Cuenta con los métodos de: Insertar, obtener, modificar y eliminar una Categoría.
    /// </summary>
    public class CategoriaRepository
    {
        /// <summary>
        /// Este método delega la responsabilidad de obtener todas las categrías almecenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de categorías.
        /// </summary>
        /// <returns>Lista de objetos Categoría. El objeto categoría posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria:</returns>
        public Categoria[] getAllCategorias()
        {
            return DAOCategoria.ObtenerCategorias();
        }

        /// <summary>
        /// Este método delega la responsabilidad de insertar una categoria en la base de datos. 
        /// Recibe por parámetro un objeto categoria.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria,nombreCategoria, descripcionCategoria </param>
        public bool SaveCategoria(Categoria categoria)
        {
            try
            {
                DAOCategoria.InsertarCategoria(categoria);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Este método delega la responsabilidad de obtener una categoría almacenada en la base de datos.
        /// Recibe por parámetro el id de categoría a consultar y devuelve la categoría que se corresponde con ese id.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idCategoria, nombreCategoria, descripcionCategoria)</returns>
        public Categoria ObtenerCategorias(int idCategoria)
        {
            return DAOCategoria.ObtenerCategorias(idCategoria);
        }

        /// <summary>
        /// Este método delega la responsabilidad de eliminar una categoria en la base de datos. 
        /// Recibe por parámetro el id de la categoría a eliminar.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a eliminar. Por ejemplo: 27</param>
        public void EliminarCategoria(int idCategoria)
        {
            DAOCategoria.EliminarCategoria(idCategoria);
        }

        /// <summary>
        /// Este método delega la responsabilidad de actualizar una categoria en la base de datos. 
        /// Recibe por parámetro un objeto categoria.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria </param>
        public void ActualizarCategoria(Categoria idCategoria)
        {
            DAOCategoria.ActualizarCategoria(idCategoria);
        }
    }
}
