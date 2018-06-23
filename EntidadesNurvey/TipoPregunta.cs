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
    /// Esta clase contiene los atributos que definen a un Tipo de Pregunta.
    /// </summary>
    public class TipoPregunta
    {
        public int idTipoPregunta { get; set; }
        public string descripcion { get; set; }
        public string type { get; set; }
    }
}
