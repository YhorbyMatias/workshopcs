using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Transactions;
using PuntoVenta.AccesoDatos;
namespace PuntoVenta.CapaNegocios
{
    public static class FacturaBO
    {
        public static int RegistrarFacturacion(Factura factura)
        {
            int folio;
            using(TransactionScope scope = new TransactionScope())
            {
                folio = FacturaDAL.Crear(factura);
                FacturaDAL.ActualizarTotal(factura.Folio, factura.Total);
                scope.Complete();
            };
            return folio;
        }

        public static void ActualizarFactura(Factura factura)
        {
            FacturaDAL.ActualizarFactura(factura);

        }

        public static Factura BuscarPorFolio(int folio)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
                Factura facturaADevolver = FacturaDAL.DevolverPorId(folio);

                //scope.Complete();
                return facturaADevolver;
            //};
        }
    }
}
