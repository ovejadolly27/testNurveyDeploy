using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesNurvey
{
    public class ResultadosGraficos
    {
        public List<string> labels { get; set; }
        public List<double> series { get; set; }
        public int cantidadTotalRespuestas { get; set; }
        public DateTime ultimaActualizacion { get; set; }

        public ResultadosGraficos()
        {
            labels = new List<string>();
            series = new List<double>();
        }
    }
}
