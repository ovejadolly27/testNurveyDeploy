using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntidadesNurvey;
using DAONurvey;

namespace NurveyProyectCore.Services
{
    public class UsuarioRepository
    {
        public bool SaveUsuario(Usuario usuario)
        {
            try
            {
                usuario.fechaAlta = DateTime.Now;
                DAOUsuario.InsertarUsuario(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        public Usuario[] GetAllUsuarios(string filtro)
        {
            return DAOUsuario.ObtenerUsuarios(filtro);
        }

        public Usuario[] GetAllUsuarios()
        {
            return DAOUsuario.ObtenerUsuarios(null);
        }

        public Usuario GetUsuario(int idUsuario)
        {
            return DAOUsuario.ObtenerUsuario(idUsuario);
        }

        public void EliminarUsuario(int idUsuario)
        {
            DAOUsuario.EliminarUsuario(idUsuario);
        }

        public Usuario ObtenerUsuario(int id)
        {
            return DAOUsuario.ObtenerUsuario(id);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            DAOUsuario.ActualizarUsuario(usuario);
        }

        public Usuario GetUsuarioAutenticado(string emailUsuario, string passwordUsuario)
        {
            return DAOUsuario.ObtenerUsuarioAutenticado(emailUsuario, passwordUsuario);
        }

        public Usuario GetValidarEmailUsuario(string emailUsuario)
        {
            return DAOUsuario.ValidarEmailUsuario(emailUsuario);
        }
    }
}
