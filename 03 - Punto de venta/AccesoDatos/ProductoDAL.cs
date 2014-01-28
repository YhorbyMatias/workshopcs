using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;

namespace PuntoVenta.AccesoDatos
{
    public abstract class ProductoDAL : AccesoDatos
    {
        public static List<Producto> DevolverTodos()
        {
            List<Producto> lista = new List<Producto>(); ;

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = @"SELECT p.IdProducto, p.Nombre,p.IdMarca, p.IdUnidadMedida,p.Cantidad,p.PrecioCompra,p.PrecioVenta,
                                p.IdCategoria,p.Descripcion,p.CantidadMayoreo,p.Foto, prp.IdProveedor FROM Productos AS p
                                JOIN Proveedor_Producto as prp ON p.IdProducto=prp.IdProducto 
                                ORDER BY p.Nombre";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    lista.Add(CargarProducto(reader));
            }
            return lista;
        }

//        public static Producto BuscarPorId(string id)
//        {
//            Producto p = null;
//            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
//            {
//                conn.Open();
//                SqlCommand cmd = new SqlCommand(@"SELECT IdProducto,Nombre,PrecioVenta,PrecioCompra,Cantidad,
//                                IdCategoria,IdUnidadMedida FROM Productos WHERE IdProducto=@idproducto", conn);
//                cmd.Parameters.AddWithValue("@idproducto", id);
//                IDataReader reader = cmd.ExecuteReader();
//                if (reader.Read())
//                    p = CargarProducto(reader);
//            }
//            return p;
//        }


        public static Producto BuscarPorId(string id)
        {
            Producto p = null;
            List<Producto> lista = DevolverTodos();
            p = (from x in lista
                 where x.IdProducto==id
                 select x).SingleOrDefault();
            return p;
        }
        public static decimal GetPrecioPorIdProducto(string id)
        {
            decimal precio = 0;

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string sql = @"SELECT PrecioVenta FROM Producto WHERE IdProducto = @idproducto";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idproducto", Convert.ToInt32(id));
                precio = Convert.ToDecimal(cmd.ExecuteScalar());
            }
            return precio;
        }

        public static void AgregarExistencia(string idProducto, int existenciasParaAgregar,string usuarioResponsable)
        {

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"UPDATE Productos SET Cantidad=Cantidad+@existenciaparaagregar WHERE IdProducto=@idproducto");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@existenciaparaagregar",existenciasParaAgregar);
                cmd.Parameters.AddWithValue("@idproducto", idProducto);
                cmd.ExecuteNonQuery();
                sb.Clear();
                cmd.Parameters.Clear();
                sb.Append("INSERT INTO Productos_Entrada(IdProducto,UserName,Fecha,Cantidad) VALUES (@idproducto,@usuarioresponsable,@fecha,@existenciaparaagregar)");
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@idproducto", idProducto);
                cmd.Parameters.AddWithValue("@usuarioresponsable", usuarioResponsable);
                cmd.Parameters.AddWithValue("@existenciaparaagregar", existenciasParaAgregar);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.ExecuteNonQuery();
                try
                {
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("Error al agregar existencias", ex);
                }
                finally
                {
                    tran.Dispose();
                    cmd.Dispose();
                    sb = null;
                }
            }
        }

        public static void ActualizarExistencia(string idProducto, int nuevaExistencia)
        {

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"UPDATE Productos SET Cantidad=@nuevaexistencia WHERE IdProducto=@idproducto");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@nuevaexistencia", nuevaExistencia);
                cmd.Parameters.AddWithValue("@idproducto", idProducto);
                cmd.ExecuteNonQuery();
                try
                {
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception("Error al actualizar nueva existencia", ex);
                }
                finally
                {
                    tran.Dispose();
                    cmd.Dispose();
                    sb = null;
                }
            }
        }

        public static bool Crear(Producto producto)
        {
            bool creado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"INSERT INTO [Productos] (IdProducto,Nombre,IdUnidadMedida,Cantidad,PrecioCompra,PrecioVenta,IdCategoria,IdMarca,Descripcion,Foto,CantidadMayoreo) 
                     VALUES (@idproducto,@nombre,@idmedida,@cantidad,@preciocom,@precioven,@idcategoria,@idmarca,@descripcion,@foto,@cantidadmayoreo) 
                    SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                SqlTransaction tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idproducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@idmedida", producto.UnidadMedida.IdUnidadMedida);
                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);
                cmd.Parameters.AddWithValue("@preciocom", producto.PrecioCompra);
                cmd.Parameters.AddWithValue("@precioven", producto.PrecioVenta);
                cmd.Parameters.AddWithValue("@idcategoria", producto.Categoria.IdCategoria);
                cmd.Parameters.AddWithValue("@idmarca", producto.Marca.Id);
                cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                if (producto.Foto == null)
                {
                    cmd.Parameters.AddWithValue("@foto", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@foto", producto.Foto);
                }
                cmd.Parameters.AddWithValue("@cantidadmayoreo", producto.CantidadMayoreo);
                cmd.ExecuteNonQuery();

                /*AQUI SE GUARDA EN LA TABLAS DE REGISTRO
                 * */
                cmd.CommandText = @"INSERT INTO Usuario_Producto_Entrada (UserName,IdProducto,Fecha,Cantidad)
                                  VALUES(@username,@idproducto,@fecha,@cantidad)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@username",producto.GetUsuarioResponsable().UserName);
                cmd.Parameters.AddWithValue("@idproducto", producto.IdProducto);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@cantidad", producto.Cantidad);

                cmd.ExecuteNonQuery();
                /*
                 * AQUI SE LE ASIGNA AL PRODUCTO SU PROVEEDOR
                 * */
                cmd.CommandText = @"INSERT INTO Proveedor_Producto(IdProveedor,IdProducto) VALUES
                                    (@idproveedor,@idproducto)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idproveedor", producto.Proveedor.IdProveedor);
                cmd.Parameters.AddWithValue("@idproducto", producto.IdProducto);
                cmd.ExecuteNonQuery();

                try
                {
                    tran.Commit();
                    creado = true;
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            return creado;
        }

        public static bool Actualizar(Producto p)
        {
            bool resultado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                StringBuilder sb = new StringBuilder();
                sb.Append(@"UPDATE [Productos] SET Nombre=@nombre, IdUnidadMedida=@idmedida,
                        PrecioCompra=@preciocom,PrecioVenta=@precioven,IdCategoria=@idcategoria,IdMarca=@idmarca,
                        Descripcion=@descripcion,Foto=@foto,CantidadMayoreo=@cantidadmayoreo
                        WHERE IdProducto=@idproducto");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@idmedida", p.UnidadMedida.IdUnidadMedida);
                cmd.Parameters.AddWithValue("@preciocom", p.PrecioCompra);
                cmd.Parameters.AddWithValue("@precioven", p.PrecioVenta);
                cmd.Parameters.AddWithValue("@idcategoria", p.Categoria.IdCategoria);
                cmd.Parameters.AddWithValue("@idmarca", p.Marca.Id);
                cmd.Parameters.AddWithValue("@descripcion", p.Descripcion);

                if (p.Foto == null)
                    cmd.Parameters.AddWithValue("@foto", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@foto", p.Foto);

                cmd.Parameters.AddWithValue("@cantidadmayoreo", p.CantidadMayoreo);

                cmd.Parameters.AddWithValue("@idproducto", p.IdProducto);

                int result = cmd.ExecuteNonQuery();
                resultado = result > 0;
                cmd.Parameters.Clear();
                sb.Clear();
                sb.Append("UPDATE [Proveedor_Producto] SET IdProveedor=@idproveedor WHERE IdProducto=@idproducto");
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@idproveedor", p.Proveedor.IdProveedor);
                cmd.Parameters.AddWithValue("@idproducto", p.IdProducto);
                cmd.ExecuteNonQuery();
                try
                {
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }

            return resultado;
        }

        public static bool Eliminar(string idProducto,string usuarioResponsable)
        {
            bool hecho=false;
            using(SqlConnection conn=new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                StringBuilder sb = new StringBuilder(@"DELETE FROM Productos WHERE IdProducto=@idproducto");
                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                SqlTransaction tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                cmd.Parameters.AddWithValue("@idproducto", idProducto);
                cmd.ExecuteNonQuery();
                sb.Clear(); 
                sb.Append("INSERT INTO Productos_Eliminados(IdProducto,UserName,Fecha) VALUES (@idproducto,@usuarioresponsable,@fecha)");
                cmd.CommandText = sb.ToString();
                cmd.Parameters.AddWithValue("@usuarioresponsable", usuarioResponsable);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.ExecuteNonQuery();

                try
                {
                    tran.Commit();
                    hecho = true;
                }
                catch (Exception excepcion)
                {
                    tran.Rollback();
                    throw new Exception("Error al eliminar producto", excepcion);
                }
                finally
                {
                    cmd = null;
                    sb = null;
                }
            }
            return hecho;
        }

        private static Producto CargarProducto(IDataReader reader)
        {
            Producto producto = new Producto();

            producto.IdProducto = reader["IdProducto"].ToString();
            producto.Nombre = Convert.ToString(reader["Nombre"]);
            producto.Cantidad = Convert.ToInt32(reader["Cantidad"]);
            producto.PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"]);
            producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);
            int idCategoria = 0;
            idCategoria = Convert.ToInt32(reader["IdCategoria"]);
            producto.Categoria = CategoriaDAL.DevolverPorId(idCategoria);
            producto.UnidadMedida = UnidadMedidaDAL.DevolverPorId((int)reader["IdUnidadMedida"]);
            producto.Proveedor = ProveedorDAL.DevolverPorId(Convert.ToString(reader["IdProveedor"]));
            //if (reader["IdMarca"] == DBNull.Value)
            //    producto.Marca = null;
            //else
                producto.Marca = MarcaDAL.DevolverPorId(Convert.ToInt32(reader["IdMarca"]));

            producto.Descripcion = reader["Descripcion"].ToString();
            if (reader["CantidadMayoreo"] == DBNull.Value)
                producto.CantidadMayoreo = 0;
            else
                producto.CantidadMayoreo = Convert.ToInt32(reader["CantidadMayoreo"]);

            if (reader["Foto"] == DBNull.Value)
                producto.Foto = "";
            else
                producto.Foto = reader["Foto"].ToString();
            return producto;
        }
    }
}
