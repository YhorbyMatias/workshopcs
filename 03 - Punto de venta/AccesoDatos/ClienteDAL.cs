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
    public abstract class ClienteDAL : AccesoDatos
    {
        static string _campos = @"IdCliente, Nombre, Apellido, Cedula,Email,Direccion,Provincia";

        private static string Campos
        {
            get { return ClienteDAL._campos; }
            set { ClienteDAL._campos = value; }
        }

        
        public static List<Cliente> DevolverTodos()
        {
            List<Cliente> list = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = string.Format("SELECT {0} FROM Clientes ORDER BY Apellido",Campos);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(CargarCliente(reader));
            }
            return list;
        }

        public static Dictionary<string,Cliente> ObtenerTodos()
        {
            Dictionary<string, Cliente> list = new Dictionary<string, Cliente>();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = string.Format("SELECT {0} FROM Clientes ORDER BY Apellido",Campos);
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = CargarCliente(reader);
                    list.Add(cliente.Id, cliente);
                }
            }
            return list;
        }

        public static Cliente BuscarPorId(string id)
        {
            Cliente customer = null;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = string.Format("SELECT {0} FROM Clientes WHERE IdCliente = @IdCliente",Campos);
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    customer = CargarCliente(reader);
            }
            return customer;
        }

        public static bool ExisteCedula(string cedula)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"SELECT COUNT(*) FROM Clientes
                                WHERE Cedula=@Cedula";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@Cedula", cedula);
                resultado = (int)cmd.ExecuteScalar();
            }
            return resultado > 0;
        }

        public static bool Existe(string id)
        {
            int nrorecord = 0;

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = @"SELECT Count(*) FROM Clientes WHERE IdCliente = @IdCliente";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCliente", id);
                nrorecord = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return nrorecord > 0;
        }

        public static void Crear(Cliente cliente)
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Clientes (IdCliente, Nombre, Apellido, Cedula,Email,Direccion,Provincia) 
                            VALUES (@IdCliente,@Nombre,@Apellido,@Cedula,@Email,@Direccion,@Provincia)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.Id);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Email",cliente.Email);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Provincia",cliente.Provincia);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool Actualizar(Cliente cliente)
        {
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = @"UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, 
                                            Cedula = @Cedula, Direccion=@Direccion, Email=@Email,
                                            Provincia=@Provincia
                                    WHERE IdCliente=@IdCliente";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("@IdCliente", cliente.Id);
                resultado = cmd.ExecuteNonQuery();
            }
            return resultado > 0;
        }

        public static bool Eliminar(string id)
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"DELETE FROM Clientes WHERE IdCliente=@id";
                SqlCommand cmd = new SqlCommand(varSql,conn);
                cmd.Parameters.AddWithValue("@id", id);
                int resultado = cmd.ExecuteNonQuery();

                return resultado > 0;
            }
        }

        private static Cliente CargarCliente(IDataReader reader)
        {
            Cliente cliente = new Cliente();
            cliente.Id = reader["IdCliente"].ToString();
            cliente.Nombre = Convert.ToString(reader["Nombre"]);
            cliente.Apellido = Convert.ToString(reader["Apellido"]);
            cliente.Cedula = Convert.ToString(reader["Cedula"]);
            cliente.Email = (String)reader["Email"];
            cliente.Direccion = Convert.ToString(reader["Direccion"]);
            cliente.Provincia = (String)reader["Provincia"];
            return cliente;
        }

    }
}
