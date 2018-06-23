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
    /// Esta clase contiene los atributos que definen a una Respuesta.
    /// </summary>
    public class Respuestas
    {
        public int idRespuesta { get; set; }
        public int idPregunta { get; set; }
        public int idEncuesta { get; set; }
        public int idEncuestado { get; set; }
        public string codigoPregunta { get; set; }
        public String descripcionRespuesta { get; set; }
        public DateTime fechaRespuesta { get; set; }
    }
}
