using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesNurvey;
using System.Data.SqlClient;

namespace DAONurvey
{
    public class DAOCategoria
    {
        public static Categoria[] ObtenerCategorias()
        {
            List<Categoria> listaCategoria = new List<Categoria>();

            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Categorias";
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    //categoria.nombre = dr["nombreCategoria"].ToString();
                    categoria.descripcion = dr["descripcionCategoria"].ToString();
                    listaCategoria.Add(categoria);
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

            return listaCategoria.ToArray<Categoria>();
        }
        public static void InsertarCategoria(Categoria categoria)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Insert into Categorias (nombreCategoria, descripcionCategoria) values
                                                    (@nombreCategoria, @descripcionCategoria)";
                //cmd.Parameters.AddWithValue("@nombreCategoria", categoria.nombre);
                cmd.Parameters.AddWithValue("@descripcionCategoria", categoria.descripcion);
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
        public static void ActualizarCategoria(Categoria categoria)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Update Categorias set nombreCategoria=@nombreCategoria, descripcionCategoria=@descripcionCategoria where idCategoria=@idCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", categoria.idCategoria);
                //cmd.Parameters.AddWithValue("@nombreCategoria", categoria.nombre);
                cmd.Parameters.AddWithValue("@descripcionCategoria", categoria.descripcion);
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

        public static void EliminarCategoria(int idCategoria)
        {
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Delete from Categorias where idCategoria=@idCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
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

        //obtener tipo de pregunta para cargar combobox
        public static Categoria ObtenerCategorias(int idCategoria)
        {
            Categoria categoria = new Categoria();
            try
            {
                SqlConnection cn = new SqlConnection(CadenaConexion.cadenaConexion);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Select * From Categorias where idCategoria=@idCategoria";
                cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    categoria.idCategoria = int.Parse(dr["idCategoria"].ToString());
                    //categoria.nombre = dr["nombreCategoria"].ToString();
                    categoria.descripcion = dr["descripcionCategoria"].ToString();
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

            return categoria;
        }

    }
}
