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
    public class ElementSurvey
    {
        public string type { get; set; }
        public string isRequired { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public List<string> choices { get; set; }
        public List<string> columns { get; set; }
        public List<string> rows { get; set; }
        public string html { get; set; }
    }
}
