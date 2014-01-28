using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace PuntoVenta.AccesoDatos
{
    public class MarcaDAL:AccesoDatos
    {
        public static List<Marca> DevolverTodos()
        {
            List<Marca> ListaMarcas=new List<Marca>();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                StringBuilder varSql = new StringBuilder();
                varSql.Append("SELECT IdMarca,Descripcion FROM Marcas");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = varSql.ToString();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        ListaMarcas.Add(CargarMarca(reader));
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al obtener las marcas", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener las marcas", ex);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Dispose();
                    varSql = null;
                }
            }
            return ListaMarcas;
        }

        public static Marca DevolverPorId(int id)
        {
            List<Marca> lista = DevolverTodos();
            Marca _marca = (from x in lista
                            where x.Id == id
                            select x).SingleOrDefault();
            return _marca;
        }

        public static int Crear(Marca _marca)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"INSERT INTO Marcas(Descripcion) VALUES (@descripcion) 
                SELECT SCOPE_IDENTITY()");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@descripcion", _marca.Descripcion);
                try
                {
                    resultado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sb = null;
                    conn.Dispose();
                    cmd.Dispose();
                }
            }
            return resultado;
        }
        private static Marca CargarMarca(IDataReader reader)
        {
            Marca marca = new Marca
            {
                Id = (int)reader["IdMarca"],
                Descripcion = reader["Descripcion"].ToString()
            };
            return marca;
        }

        public static bool Modificar(Marca m)
        {
            bool modificado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"UPDATE Marcas SET Descripcion=@descripcion WHERE IdMarca=@idmarca");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@descripcion", m.Descripcion);
                cmd.Parameters.AddWithValue("idmarca", m.Id);
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    modificado = result > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error al actualizar marca", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar marca", ex);
                }
                finally
                {
                    conn.Dispose();
                    cmd.Dispose();
                    sb = null;
                }
            }
            return modificado;
        }

        public static bool Eliminar(string id)
        {
            bool eliminado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"DELETE FROM Marcas WHERE IdMarca=@idmarca");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("idmarca", id);
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    eliminado = result > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar la marca", ex);
                }
                finally
                {
                    conn.Dispose();
                    cmd.Dispose();
                    sb = null;
                }
            }
            return eliminado;
        }

    }
}
