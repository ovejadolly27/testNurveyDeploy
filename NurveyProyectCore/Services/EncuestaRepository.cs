using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class EncuestaRepository
    {
        public int SaveEncuesta(Encuesta encuesta)
        {
            try
            {
                return DAOEncuesta.InsertarEncuesta(encuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }

        }
        public Encuesta[] GetAllEncuestas()
        {
            return DAOEncuesta.ObtenerEncuestas();
        }

        public void EliminarEncuesta(int idEncuesta)
        {
            DAOEncuesta.EliminarEncuesta(idEncuesta);
        }

        public Encuesta ObtenerEncuesta(int id)
        {
            return DAOEncuesta.ObtenerEncuesta(id);
        }

        public Encuesta[] ObtenerEncuestasPorUsuario(int id)
        {
            return DAOEncuesta.ObtenerEncuestasPorUsuario(id);
        }

        public void ActualizarEncuesta(Encuesta encuesta)
        {
            DAOEncuesta.ActualizarEncuesta(encuesta);
        }

        public void ActualizarEstadoEncuesta(string estadoEncuesta, int idEncuesta, int idUsuario)
        {
            DAOEncuesta.ActualizarEstadoEncuesta(estadoEncuesta, idEncuesta, idUsuario);
        }

        public void ActualizarPublicado(bool publicado, int idEncuesta, int idUsuario)
        {
            DAOEncuesta.ActualizarPublicado(publicado, idEncuesta, idUsuario);
        }

        public Encuesta[] ObtenerEncuestasPorTitulo(string filtro)
        {
            return DAOEncuesta.ObtenerEncuestasPorTitulo(filtro);
        }

        public Encuesta[] ObtenerEncuestasPorPublicado(bool publicado)
        {
            return DAOEncuesta.ObtenerEncuestasPorPublicado(publicado);
        }

        public Encuesta[] ObtenerEncuestasPorPublicadoYEstado(bool publicado, string estadoEncuesta)
        {
            return DAOEncuesta.ObtenerEncuestasPorPublicadoYEstado(publicado, estadoEncuesta);
        }

        public Encuesta[] ObtenerEncuestasPorEstadoEncuesta(string estadoEncuesta)
        {
            return DAOEncuesta.ObtenerEncuestasPorEstadoEncuesta(estadoEncuesta);
        }
    }
}
