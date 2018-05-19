using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
    public class Pregunta
    {
        public int idPregunta { get; set; }

        public string name { get; set; }

        public string descripcion { get; set; }

        public int idTipoPregunta { get; set; }

        public int idCategoria { get; set; }

        public int idEncuesta { get; set; }

        public bool esAgrupable { get; set; }
    }
}
