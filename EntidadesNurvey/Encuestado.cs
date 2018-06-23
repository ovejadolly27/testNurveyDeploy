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
    /// Esta clase contiene los atributos que definen a un Encuestado.
    /// </summary>
    public class Encuestado
    {
        public int idEncuestado { get; set; }
        public DateTime tiempoRespuesta { get; set; }
        public String ubicacion { get; set; }
    }
}
