using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class TipoPreguntaRepository
    {
        public TipoPregunta[] GetAllTiposPreguntas()
        {
            return DAOTipoPregunta.ObtenerTiposPreguntas();
        }
        public bool SaveTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                DAOTipoPregunta.InsertarTipoPregunta(tipoPregunta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public TipoPregunta ObtenerTipoPregunta(int id)
        {
            return DAOTipoPregunta.ObtenerTipoPregunta(id);
        }

        public void EliminarTipoPregunta(int idTipoPregunta)
        {
            DAOTipoPregunta.EliminarTipoPregunta(idTipoPregunta);
        }

        public void ActualizarTipoPregunta(TipoPregunta tipoPregunta)
        {
            DAOTipoPregunta.ActualizarTipoPregunta(tipoPregunta);
        }
    }
}
