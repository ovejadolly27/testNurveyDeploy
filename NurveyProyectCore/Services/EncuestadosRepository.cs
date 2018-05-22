using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class EncuestadosRepository
    {
        public Encuestado[] getAllEncuestados()
        {
            return DAOEncuestado.ObtenerEncuestados();
        }

        public bool SaveEncuestados(Encuestado encuestado)
        {
            try
            {
                DAOEncuestado.InsertarEncuestado(encuestado);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
