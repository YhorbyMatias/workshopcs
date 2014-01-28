using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace PuntoVenta.AccesoDatos
{
    public abstract class CajaDAL:AccesoDatos
    {
        public static int Crear(Caja caja)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                string varSql = @"INSERT INTO Cajas(Descripcion,Disponible,UserName) VALUES (@desc,@disp,@username)
                                SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@desc", caja.Descripcion);
                cmd.Parameters.AddWithValue("@disp", caja.Disponible);
                cmd.Parameters.AddWithValue("@username", caja.Usuario.UserName);
                resultado = (int)cmd.ExecuteScalar();
            }
            return resultado;
        }

        public static bool AsignarCaja(int idCaja, string userName)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                conn.Open();
                string varSql = @"UPDATE Cajas SET=Usuario=@usuario WHERE IdCaja=@idcaja";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@usuario",userName);
                cmd.Parameters.AddWithValue("@idcaja", idCaja);
                int resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
        }

        public static bool EstablecerEstado(int id,bool estado)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ConnectionString))
            {
                string varSql = @"UPDATE Cajas SET Disponible=@disp WHERE IdCaja=@idcaja";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@disp", estado);
                cmd.Parameters.AddWithValue("@idcaja", id);
                int resultado = 0;
                resultado = cmd.ExecuteNonQuery();
                return resultado > 0;
            }
        }


    }
}
