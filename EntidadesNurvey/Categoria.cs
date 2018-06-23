using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// Este proyecto contiene las clases que modelan un objeto de la base de datos.
/// Se basa en el lenguaje C#.
/// </summary>
namespace EntidadesNurvey
{
    /// <summary>
    /// Esta clase contiene los atributos que definen a una Categoría.
    /// </summary>
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }
}

