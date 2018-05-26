using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class ResultadosRepository
    {
        public static ResultadosGraficos ObtenerResultados(int idEncuesta, int idPregunta)
        {

            return DAOResultados.ObtenerDatos(idEncuesta, idPregunta);
        }

        internal static ResultadosGraficos ObtenerResultadosEncuestasXUsuario(int idUsuario)
        {
            return DAOResultados.ObtenerDatosEncuestasXUsuario(idUsuario);
        }
    }
}
