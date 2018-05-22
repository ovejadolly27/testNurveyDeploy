using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class RespuestasRepository
    {
        public bool SaveRespuesta(RespuestaEncuestado respuestaEncuestado)
        {
            List<Respuestas> listaRespuestas = respuestaEncuestado.listaRespuestas;
            Encuestado encuestado = respuestaEncuestado.encuestado;
            try
            {
                DAORespuestas.InsertarRespuestas(listaRespuestas, encuestado);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Respuestas[] getAllRespuestas()
        {
            return DAORespuestas.ObtenerRespuestas();
        }
    }
}

