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
    /// Esta clase contiene los atributos que definen a un Usuario.
    /// </summary>
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string emailUsuario { get; set; }
        public string passwordUsuario { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime ultimaEncuesta { get; set; }
        public int encuestasCreadas { get; set; }
    }
}
