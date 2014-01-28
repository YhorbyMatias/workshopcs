using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace PuntoVenta.AccesoDatos
{
    public class UnidadMedidaDAL:AccesoDatos
    {
        public static UnidadMedida DevolverPorId(int id)
        {
            UnidadMedida UnidadMedida= new UnidadMedida();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"SELECT IdUnidadMedida,Descripcion FROM UnidadMedida WHERE IdUnidadMedida=@idUnidadMedida";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@idUnidadMedida", id);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    UnidadMedida= CargarUnidadMedida(reader);
            }
            return UnidadMedida;
        }

        public static List<UnidadMedida> DevolverTodos()
        {
            List<UnidadMedida> _UnidadMedida = null;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                _UnidadMedida = new List<UnidadMedida>();
                string varSql = @"SELECT IdUnidadMedida,Descripcion FROM UnidadMedida";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    _UnidadMedida.Add(CargarUnidadMedida(reader));
            }
            return _UnidadMedida;
        }

        public static int Crear(UnidadMedida UnidadMedida)
        {
            //PAra devolver el Id generado
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"INSERT INTO UnidadMedida(Descripcion) VALUES (@descripcion);
                                SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@descripcion", UnidadMedida.Descripcion);
                resultado = (int)cmd.ExecuteScalar();
            }
            return resultado;
        }

        public static bool Existe(UnidadMedida UnidadMedida)
        {
            bool existe = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                //El LIKE %valor% devuelve todos los registros que contengan el valor
                string varSql = string.Format(@"SELECT COUNT(*) FROM UnidadMedida WHERE Descripcion LIKE %{0}%", UnidadMedida.Descripcion);
                SqlCommand cmd = new SqlCommand(varSql, conn);
                int resultado = 0;
                /*El executeNonQuery devuelve un entero con la cantidad de registros que afectó
                 **/
                resultado = cmd.ExecuteNonQuery();
                //Entonces si resultado es mayor que cero es que existe :/
                existe = resultado > 0;
            }
            return existe;
        }

        public static bool Actualizar(UnidadMedida UnidadMedida)
        {
            bool actualizado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                int resultado = 0;
                string varSql = @"UPDATE UnidadMedida SET Descripcion=@descripcion 
                                WHERE IdUnidadMedida=@idUnidadMedida";
                SqlCommand cmd = new SqlCommand(varSql, conn);

                resultado = cmd.ExecuteNonQuery();

                actualizado = resultado > 0;
            }
            return actualizado;
        }

        private static UnidadMedida CargarUnidadMedida(IDataReader reader)
        {
            UnidadMedida UnidadMedida= new Entidades.UnidadMedida();
            UnidadMedida.IdUnidadMedida= (int)reader["IdUnidadMedida"];
            UnidadMedida.Descripcion = reader["Descripcion"].ToString();
            return UnidadMedida;
        }
    }
}
