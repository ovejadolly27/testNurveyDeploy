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
    /// Esta clase contiene los atributos que modelan un listado de las Respuestas de un Encuestado.
    /// </summary>
    public class RespuestaEncuestado
    {
        public List<Respuestas> listaRespuestas { get; set; }
        public Encuestado encuestado { get; set; }
    }
}
