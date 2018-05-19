using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;



namespace NurveyProyectCore.Services
{
    public class PreguntaRepository
    {
        public bool SavePregunta(Pregunta pregunta)
        {
            try
            {
                DAOPreguntas.InsertarPregunta(pregunta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Pregunta ObtenerPregunta(int idPregunta)
        {
            return DAOPreguntas.ObtenerPregunta(idPregunta);
        }

        public void EliminarPregunta(int idPregunta)
        {
            DAOPreguntas.EliminarPregunta(idPregunta);
        }

        public void ActualizarPregunta(Pregunta pregunta)
        {
            DAOPreguntas.ActualizarPregunta(pregunta);
        }

        public Pregunta[] GetAllPreguntas(string filtro)
        {
            return DAOPreguntas.ObtenerPreguntas(filtro);
        }

        public Pregunta[] GetAllPreguntas()
        {
            return DAOPreguntas.ObtenerPreguntas(null);
        }

        //Filtro por id de cateogoria
        public Pregunta[] GetPreguntasCategoria(int idCategoria)
        {
            return DAOPreguntas.ObtenerPreguntasCategoria(idCategoria);
        }

        //Filtro por id de tipo pregunta
        public Pregunta[] GetPreguntasTipo(int idTipoPregunta)
        {
            return DAOPreguntas.ObtenerPreguntasTipo(idTipoPregunta);
        }
    }
}
