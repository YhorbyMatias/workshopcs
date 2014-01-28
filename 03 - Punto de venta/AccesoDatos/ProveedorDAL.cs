using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace PuntoVenta.AccesoDatos
{
    public class ProveedorDAL:AccesoDatos
    {
        public static void Crear(Proveedor proveedor)
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"INSERT INTO Proveedores(IdProveedor,Nombre,Telefono,Direccion,Email)
                                VALUES(@idproveedor,@nombre,@telefono,@direccion,@email)";
                using (SqlCommand cmd = new SqlCommand(varSql, conn))
                {
                    cmd.Parameters.AddWithValue("@idproveedor", proveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@email", proveedor.Email);

                    cmd.ExecuteNonQuery();
                }

            }
        }

        private static bool ChequearProducto(int idProveedor,int idProducto)
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                string varSql = @"SELECT COUNT(*) FROM Proveedor_Producto WHERE
                                 IdProveedor=@idproveedor AND IdProducto=@idproducto";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@idproveedor", idProveedor);
                cmd.Parameters.AddWithValue("@idproducto", idProducto);
                int resultado = 0;
                resultado = (int)cmd.ExecuteScalar();
                return resultado > 0;
            }
        }

        public static List<Proveedor> DevolverTodos()
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"SELECT IdProveedor,Nombre,Telefono,Direccion,Email FROM Proveedores";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                List<Proveedor> listaProveedores = new List<Proveedor>();
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Proveedor p = CargarProveedor(reader);
                    listaProveedores.Add(p);
                }
                return listaProveedores;
            }
        }

        public static void Actualizar(Proveedor proveedor)
        {
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                SqlTransaction tran = conn.BeginTransaction();
                conn.Open();
                string varSql = @"UPDATE Proveedores SET Nombre=@nombre,Telefono=@telefono,Direccion=@direccion,
                                Email=@email WHERE IdProveedor=@idproveedor";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@nombre", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);
                cmd.Parameters.AddWithValue("@direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@email", proveedor.Email);
                cmd.Parameters.AddWithValue("@idproveedor", proveedor.IdProveedor);
                cmd.ExecuteNonQuery();
                tran.Save("update1");
                //cmd.Parameters.Clear();
                //string sql=@"INSERT INTO Proveedor_Producto(IdProveedor,IdProducto) VALUES(@idproveedor,@idproducto)";
                //cmd.CommandText = sql;
                //foreach (Producto producto in proveedor.Productos)
                //{
                //if (!ChequearProducto(proveedor.IdProveedor, producto.IdProducto))
                //{
                //    cmd.Parameters.AddWithValue("@idproveedor", proveedor.IdProveedor);
                //    cmd.Parameters.AddWithValue("@idproducto", producto.IdProducto);
                //    cmd.ExecuteNonQuery();
                //}
                //}
                try
                {
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public static Proveedor DevolverPorId(string id)
        {
            List<Proveedor> _listaProveedores = DevolverTodos();
            var pro = (from x in _listaProveedores
                       where x.IdProveedor.Equals(id)
                       select x).SingleOrDefault();
            return pro;
        }
        static Proveedor CargarProveedor(IDataReader reader)
        {
            Proveedor _p = new Proveedor();
            _p.IdProveedor = (string)reader["IdProveedor"];
            _p.Nombre = (string)reader["Nombre"];
            _p.Telefono = (string)reader["Telefono"];
            _p.Direccion = (reader["Direccion"] == DBNull.Value) ? "" : (string)reader["Direccion"];
            _p.Email = (reader["Email"] == DBNull.Value) ? "" : (string)reader["Email"];
            return _p;
        }

    }
}
