using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
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
