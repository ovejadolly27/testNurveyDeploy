using System;
using System.Collections.Generic;
using System.Text;

namespace DAONurvey
{
    public class DAOMetodosUtiles
    {
        public static DateTime ParsearFecha(string campo)
        {
            DateTime fecha = DateTime.MinValue;

            if (DateTime.TryParse(campo, out fecha))
            {
                return fecha;
            }
            return fecha;
        }
    }
}
