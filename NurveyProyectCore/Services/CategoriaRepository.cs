using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAONurvey;
using EntidadesNurvey;

namespace NurveyProyectCore.Services
{
    public class CategoriaRepository
    {
        public Categoria[] getAllCategorias()
        {
            return DAOCategoria.ObtenerCategorias();
        }

        public bool SaveCategoria(Categoria categoria)
        {
            try
            {
                DAOCategoria.InsertarCategoria(categoria);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Categoria ObtenerCategorias(int idCategoria)
        {
            return DAOCategoria.ObtenerCategorias(idCategoria);
        }

        public void EliminarCategoria(int idCategoria)
        {
            DAOCategoria.EliminarCategoria(idCategoria);
        }

        public void ActualizarCategoria(Categoria idCategoria)
        {
            DAOCategoria.ActualizarCategoria(idCategoria);
        }
    }
}
