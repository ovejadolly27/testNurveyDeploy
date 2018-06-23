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
    /// Esta clase contiene los atributos que definen a definen a un Encuesta.
    /// </summary>
    public class PaginaSurvey
    {
        public string name { get; set; }
        public List<ElementSurvey> elements { get; set; }
    }
}
