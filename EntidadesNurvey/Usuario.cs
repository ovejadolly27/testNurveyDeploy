using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
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
