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
    /// Esta clase contiene los atributos que definen a una Encuesta.
    /// </summary>
    public class Encuesta
    {
        public int idEncuesta { get; set; }
        public string tituloEncuesta { get; set; }
        public int idCategoriaEncuesta { get; set; }
        public EncuestaSurvey definicion { get; set; }
        public string definicionJSON { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaEncuesta { get; set; }
        public bool publicado { get; set; }
        public string estadoEncuesta { get; set; }
    }
}
