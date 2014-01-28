using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using PuntoVenta.Entidades;
using System.Data;

namespace PuntoVenta.AccesoDatos
{
    public abstract class FacturaDAL:AccesoDatos
    {
        public static int Crear(Factura venta)
        {
            int folio = 0;

            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                //
                // Creacion de la Factura
                //
                string sqlFactura = @"INSERT INTO Venta (IdCliente, Fecha, Total,UserName,DireccionFacturacion) VALUES (@IdCliente, @Fecha, @Total,@UserName,@Direccion)
                           SELECT SCOPE_IDENTITY()";

                using (SqlCommand cmd = new SqlCommand(sqlFactura, conn))
                {

                    cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                    cmd.Parameters.AddWithValue("@Fecha", venta.Fecha);
                    cmd.Parameters.AddWithValue("@Total", venta.Total);
                    cmd.Parameters.AddWithValue("@UserName", venta.UserName);
                    cmd.Parameters.AddWithValue("@Direccion", venta.Direccion);

                    folio = venta.Folio = Convert.ToInt32(cmd.ExecuteScalar());
                }


                string sqlLineaFactura = @"INSERT INTO DetalleVenta (Folio, IdProducto, PrecioUnidad, Cantidad) 
                                            VALUES (@Folio, @IdProducto, @PrecioUnidad, @Cantidad)
                                            SELECT SCOPE_IDENTITY()";

                    foreach (DetalleFactura detalles in venta.Detalles)
                    {
                        SqlCommand cmd = new SqlCommand(sqlLineaFactura, conn);                        

                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@Folio", venta.Folio);
                        cmd.Parameters.AddWithValue("@IdProducto", detalles.IdProducto);
                        cmd.Parameters.AddWithValue("@PrecioUnidad", detalles.PrecioUnidad);
                        cmd.Parameters.AddWithValue("@Cantidad", detalles.Cantidad);

                        //
                        // se obtiene el id de linea de factura
                        //
                        detalles.IdDetalleVenta = Convert.ToInt32(cmd.ExecuteScalar());

                        //
                        // se resta la cantidad comprada de las existencias del producto
                        //
                        cmd.CommandText="UPDATE Productos SET Cantidad=Cantidad-@cantidad WHERE IdProducto=@idproducto";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idproducto",detalles.IdProducto);
                        cmd.Parameters.AddWithValue("@cantidad", detalles.Cantidad);

                        cmd.ExecuteNonQuery();
                    }

            }

            return folio;

        }

        /// <summary>
        /// Actualizacion del Total de la Factura
        /// </summary>
        public static void ActualizarTotal(int idInvoice, decimal total)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();

                string sqlUpdateTotal = @"UPDATE Venta SET Total = @total WHERE Folio = @folio";

                using (SqlCommand cmd = new SqlCommand(sqlUpdateTotal, conn))
                {
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@folio", idInvoice);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void ActualizarFactura(Factura factura)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();

                string sqlUpdateTotal = @"UPDATE Venta SET Total = @total WHERE Folio = @folio";

                using (SqlCommand cmd = new SqlCommand(sqlUpdateTotal, conn))
                {
                    SqlTransaction tran = conn.BeginTransaction();
                    cmd.Transaction = tran;

                    cmd.Parameters.AddWithValue("@total", factura.Total);
                    cmd.Parameters.AddWithValue("@folio", factura.Folio);

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();

//                    cmd.CommandText = @"UPDATE DetalleVenta SET Cantidad=@cantidad, PrecioUnidad=@preciounidad 
//                                        WHERE Folio=@folio AND IdProducto=@idproducto";

//                    cmd.CommandText = @"BEGIN TRY
//                                        INSERT INTO [DetalleVenta] (Folio,IdProducto,Cantidad,PrecioUnidad) 
//                                                    VALUES (@folio,@idproducto,@cantidad,@preciounidad)
//                                        END TRY
//                                        BEGIN CATCH
//                                        UPDATE [DetalleVenta] SET Cantidad = @cantidad,PrecioUnidad=@preciounidad
//                                                WHERE (Folio=@folio AND IdProducto=@idproducto)
//                                        END CATCH";

                    cmd.CommandText = @"MERGE INTO DetalleVenta
                                        USING (SELECT @idproducto AS IdProducto,@folio as Folio) AS SRC ON (SRC.IdProducto=DetalleVenta.IdProducto AND SRC.Folio=DetalleVenta.Folio)
                                        WHEN MATCHED THEN
                                        UPDATE SET Cantidad = @cantidad, PrecioUnidad=@preciounidad
                                        WHEN NOT MATCHED BY SOURCE THEN
                                        INSERT (Folio, IdProducto, PrecioUnidad, Cantidad) VALUES (@Folio, @IdProducto, @PrecioUnidad, @Cantidad)
                                        WHEN NOT MATCHED BY TARGET THEN
                                        DELETE;";



                    foreach (DetalleFactura detalle in factura.Detalles)
                    {

                        cmd.Parameters.AddWithValue("@folio", factura.Folio);
                        cmd.Parameters.AddWithValue("@idproducto", detalle.IdProducto);
                        cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        cmd.Parameters.AddWithValue("@preciounidad", detalle.PrecioUnidad);

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                    }

               

                    ActualizarTotal(factura.Folio, factura.Total);



                    try
                    {
                        tran.Commit();
                    }
                    catch (Exception excepcion)
                    {
                        tran.Rollback();

                        throw excepcion;
                    }
                    finally
                    {

                        tran.Dispose();
                    }
                }
            }

        }
        public static Factura DevolverPorId(int id)
        {
            Factura facturaEncontrada = null;
            using (SqlConnection conn=new SqlConnection(GlobalConnectionString))
            {
                conn.Open();

                string query= @"SELECT Folio,Fecha,Total,IdCliente,UserName FROM [Venta] WHERE Folio=@folio";
                using (SqlCommand cmd=new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@folio", id);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                        facturaEncontrada = CargarFactura(dr,conn);
                }
            }

            return facturaEncontrada;
        }


        static Factura CargarFactura(IDataReader reader,SqlConnection conn)
        {
            Factura facturaCreada = new Factura();
            facturaCreada.Folio = Convert.ToInt32(reader["Folio"]);
            facturaCreada.IdCliente = Convert.ToString(reader["IdCliente"]);
            facturaCreada.Fecha = Convert.ToDateTime(reader["Fecha"]);
            facturaCreada.Detalles = BuscarDetallesPorFolio(facturaCreada.Folio);
            facturaCreada.UserName = reader["UserName"] is DBNull ? "" : reader["UserName"].ToString();

            return facturaCreada;
        }

        static List<DetalleFactura> BuscarDetallesPorFolio(int folio)
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();

                //SqlTransaction tran = conn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(@"SELECT IdDetalleVenta,IdProducto,Folio,Cantidad,PrecioUnidad
                                                FROM [DetalleVenta] WHERE Folio=@folio", conn);

                //cmd.Transaction = tran;

                cmd.Parameters.AddWithValue("@folio", folio);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DetalleFactura detalleEncontrado = CargarDetalleFactura(dr);

                    detalles.Add(detalleEncontrado);
                }

                //try
                //{
                //    tran.Commit();
                //}
                //catch (Exception ex)
                //{
                //    tran.Rollback();
                //    throw ex;
                //}

                cmd.Dispose();
            }

            return detalles;
        }

        static DetalleFactura CargarDetalleFactura(IDataReader reader)
        {
            DetalleFactura detalleCreado = new DetalleFactura();
            detalleCreado.FolioFactura = Convert.ToInt32(reader["Folio"]);
            detalleCreado.IdProducto = Convert.ToString(reader["IdProducto"]);
            detalleCreado.Cantidad = Convert.ToInt32(reader["Cantidad"]);
            detalleCreado.PrecioUnidad = Convert.ToDecimal(reader["PrecioUnidad"]);
     
            return detalleCreado;
        }
    }
}
