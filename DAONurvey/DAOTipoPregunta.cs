using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesNurvey;
using System.Data.SqlClient;
using System.Configuration;

namespace DAONurvey
{
    public class DAOTipoPregunta
    {
        public static TipoPregunta[] ObtenerTiposPreguntas()
        {
            List<TipoPregunta> listaTipoPregunta = new List<TipoPregunta>();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas";
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TipoPregunta tipoPregunta = new TipoPregunta();
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
                    tipoPregunta.type = dr["type"].ToString();
                    listaTipoPregunta.Add(tipoPregunta);
                }

                cn.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
            return listaTipoPregunta.ToArray<TipoPregunta>();
        }
        public static void InsertarTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into TipoPreguntas (descripcionTipoPregunta) values  (@descripcionTipoPregunta, @type)";
                cmd.Parameters.AddWithValue("@descripcionTipoPregunta", tipoPregunta.descripcion);
                cmd.Parameters.AddWithValue("@type", tipoPregunta.type);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        public static void ActualizarTipoPregunta(TipoPregunta tipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update TipoPreguntas set descripcionTipoPregunta=@descripcionTipoPregunta where idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", tipoPregunta.idTipoPregunta);
                cmd.Parameters.AddWithValue("@descripcionTipoPregunta", tipoPregunta.descripcion);
                cmd.Parameters.AddWithValue("@type", tipoPregunta.type);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        public static void EliminarTipoPregunta(int idTipoPregunta)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete from TipoPreguntas where idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", idTipoPregunta);
                cmd.ExecuteNonQuery();

                cn.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }
        }

        //obtener tipo de pregunta para cargar combo
        public static TipoPregunta ObtenerTipoPregunta(int idTipoPregunta)
        {
            TipoPregunta tipoPregunta = new TipoPregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas t where t.idTipoPregunta=@idTipoPregunta";
                cmd.Parameters.AddWithValue("@idTipoPregunta", idTipoPregunta);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
                    tipoPregunta.type = dr["type"].ToString();
                }

                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return tipoPregunta;
        }

        public static TipoPregunta ObtenerTipoPregunta(string type)
        {
            TipoPregunta tipoPregunta = new TipoPregunta();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From TipoPreguntas t where t.type=@type";
                cmd.Parameters.AddWithValue("@type", type);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tipoPregunta.idTipoPregunta = int.Parse(dr["idTipoPregunta"].ToString());
                    tipoPregunta.type = dr["type"].ToString();
                    tipoPregunta.descripcion = dr["descripcionTipoPregunta"].ToString();
                }

                cn.Close();
                dr.Close();
            }

            catch (Exception ex)
            {
                string mensaje = string.Empty;
                mensaje = ex.ToString();
                Console.WriteLine(mensaje);
            }

            return tipoPregunta;
        }
    }
}
