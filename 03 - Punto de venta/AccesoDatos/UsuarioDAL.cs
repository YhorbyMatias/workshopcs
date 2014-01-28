using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PuntoVenta.AccesoDatos
{
    public class UsuarioDAL : AccesoDatos
    {
        public static bool Crear(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"INSERT INTO Usuarios(UserName,Contraseña,Nombre,Apellido,
                                Vender,Administrar,Reportes,Catalogos,Consultas,DeshacerVenta)
                                VALUES(@username,@contraseña,@nombre,@apellido,@vender,@administrar,@reportes,
                                        @catalogos,@consultas,@deshacerventa)";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@username", usuario.UserName);
                cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@vender", usuario.Vender);
                cmd.Parameters.AddWithValue("@administrar", usuario.Administrar);
                cmd.Parameters.AddWithValue("@reportes", usuario.Reportes);
                cmd.Parameters.AddWithValue("@catalogos", usuario.Catalogos);
                cmd.Parameters.AddWithValue("@consultas", usuario.Consultar);
                cmd.Parameters.AddWithValue("@deshacerventa", usuario.DeshacerVenta);
                resultado = cmd.ExecuteNonQuery();
            }
            return resultado > 0;
        }

        public static bool Modificar(Usuario usuario)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"UPDATE Usuarios SET Contraseña=@contraseña,Nombre=@nombre,Apellido=@apellido,
                                Vender=@vender,Administrar=@administrar,Reportes=@reportes,Catalogos=@catalogos,
                                Consultas=@consultas,DeshacerVenta=@deshacerventa
                                WHERE UserName=@username";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@vender", usuario.Vender);
                cmd.Parameters.AddWithValue("@administrar", usuario.Administrar);
                cmd.Parameters.AddWithValue("@reportes", usuario.Reportes);
                cmd.Parameters.AddWithValue("@catalogos", usuario.Catalogos);
                cmd.Parameters.AddWithValue("@consultas", usuario.Consultar);
                cmd.Parameters.AddWithValue("@deshacerventa", usuario.DeshacerVenta);
                cmd.Parameters.AddWithValue("@username", usuario.UserName);
                resultado = cmd.ExecuteNonQuery();
            }
            return resultado > 0;
        }
        public static Usuario DevolverPorId(string username)
        {
            Usuario usuario = null;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"SELECT UserName,Contraseña,Nombre,Apellido,
                                Vender,Administrar,Reportes,Catalogos,Consultas,DeshacerVenta
                                FROM Usuarios WHERE UserName=@username";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    usuario = CargarUsuario(reader);
            }
            return usuario;
        }
        public static List<Usuario> DevolverTodos()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"SELECT UserName,Contraseña,Nombre,Apellido,
                                Vender,Administrar,Reportes,Catalogos,Consultas,DeshacerVenta
                                FROM Usuarios ORDER BY UserName";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    usuarios.Add(CargarUsuario(reader));
            }
            return usuarios;
        }

        public static bool Comprobar(string userName, string contraseña)
        {
            Usuario usuario = DevolverPorId(userName);
            if (usuario == null)
                return false;
            return usuario.Contraseña == contraseña;
        }

        public static bool EliminarUsuario(string userName, String usuarioActual, string comentarios=" ")
        {
            bool resultado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                string varSql = @"DELETE FROM Usuarios WHERE UserName=@username";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cmd.CommandText = @"INSERT INTO Usuarios_Eliminados(UserName,Comentarios,Fecha,UsuarioResponsable)
                                    VALUES(@username,@comentarios,@fecha,@usuarioresponsable)";

                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@comentarios", comentarios);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@usuarioresponsable", usuarioActual);
                cmd.ExecuteNonQuery();
                try
                {
                    tran.Commit();
                    resultado = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }
                return resultado;
            }
        }
        static Usuario CargarUsuario(IDataReader reader)
        {
            Usuario user = new Usuario();
            user.UserName = (string)reader["UserName"];
            user.Contraseña = (string)reader["Contraseña"];
            user.Nombre = (string)reader["Nombre"];
            user.Apellido = (string)reader["Apellido"];
            user.Vender = Convert.ToBoolean(reader["Vender"]);
            user.Administrar = Convert.ToBoolean(reader["Administrar"]);
            user.Reportes = Convert.ToBoolean(reader["Reportes"]);
            user.Catalogos = Convert.ToBoolean(reader["Catalogos"]);
            user.Consultar = Convert.ToBoolean(reader["Consultas"]);
            user.DeshacerVenta = Convert.ToBoolean(reader["DeshacerVenta"]);
            return user;
        }

    }
}
