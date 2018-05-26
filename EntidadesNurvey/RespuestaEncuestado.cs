using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
    public class RespuestaEncuestado
    {
        public List<Respuestas> listaRespuestas { get; set; }
        public Encuestado encuestado { get; set; }
    }
}
