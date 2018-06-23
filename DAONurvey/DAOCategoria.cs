using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesNurvey;
using System.Data.SqlClient;

/// <summary>
/// Este proyecto contiene las clases que proporcionan el acceso a la base de datos.
/// Se basa en el lenguaje C#, utiliza SQL (Lenguaje de consulta estructurada) y sirve de interfaz de acceso hacia un servidor Microsoft SQL Server.
/// </summary>
namespace DAONurvey
{
    /// <summary>
    /// Esta clase contiene los métodos de acceso a los datos relacionados con las categorías de preguntas.
    /// Cuenta con los métodos de: Insertar, actualizar, eliminar, consultar categorías.
    /// </summary>
    public class DAOCategoria
    {
        /// <summary>
        /// Este método obtiene todas las categrías almecenadas en la base de datos.
        /// No recibe ningún parámetro y devuelve una lista de categorías.
        /// </summary>
        /// <returns>Lista de objetos Categoría. El objeto categoría posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria:</returns>
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

        /// <summary>
        /// Este método inserta una categoria en la base de datos. 
        /// Recibe por parámetro un objeto categoria.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria,nombreCategoria, descripcionCategoria </param>
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

        /// <summary>
        /// Este método actualiza una categoria en la base de datos. 
        /// Recibe por parámetro un objeto categoria.
        /// </summary>
        /// <param name="categoria">Objeto que posee los siguientes atributos: idCategoria, nombreCategoria, descripcionCategoria </param>
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

        /// <summary>
        /// Este método elimina una categoria en la base de datos. 
        /// Recibe por parámetro el id de la categoría a eliminar.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a eliminar. Por ejemplo: 27</param>
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

        /// <summary>
        /// Este método obtiene una categoría almacenada en la base de datos.
        /// Recibe por parámetro el id de categoría a consultar y devuelve la categoría que se corresponde con ese id.
        /// </summary>
        /// <param name="idCategoria">ID (int) de la categoría a consultar. Por ejemplo: 27</param>
        /// <returns>Objeto que posee los siguientes atributos: (idCategoria, nombreCategoria, descripcionCategoria)</returns>
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
