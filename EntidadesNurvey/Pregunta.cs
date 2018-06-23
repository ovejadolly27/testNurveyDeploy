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
    /// Esta clase contiene los atributos que definen a una Pregunta.
    /// </summary>
    public class Pregunta
    {
        public int idPregunta { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public int idTipoPregunta { get; set; }
        public int idCategoria { get; set; }
        public int idEncuesta { get; set; }
        public bool esAgrupable { get; set; }
    }
}
