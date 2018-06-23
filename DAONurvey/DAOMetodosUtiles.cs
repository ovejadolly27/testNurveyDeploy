using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Este proyecto contiene las clases que proporcionan el acceso a la base de datos.
/// Se basa en el lenguaje C#, utiliza SQL (Lenguaje de consulta estructurada) y sirve de interfaz de acceso hacia un servidor Microsoft SQL Server.
/// </summary>
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
