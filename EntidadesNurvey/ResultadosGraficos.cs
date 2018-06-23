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
    /// Esta clase contiene los atributos que definen a un Gráfico.
    /// </summary>
    public class ResultadosGraficos
    {
        public List<string> labels { get; set; }
        public List<double> series { get; set; }
        public int cantidadTotalRespuestas { get; set; }
        public DateTime ultimaActualizacion { get; set; }

        /// <summary>
        /// Este método inicializa los labels y series de la clase.
        /// </summary>
        public ResultadosGraficos()
        {
            labels = new List<string>();
            series = new List<double>();
        }
    }
}
