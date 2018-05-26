using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
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
