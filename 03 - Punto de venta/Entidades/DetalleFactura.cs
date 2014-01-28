using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class DetalleFactura
    {
        public DetalleFactura()
        {

        }
        public DetalleFactura(int idDetalle, int folioFactura, string idProducto, decimal precioUnidad, int cantidad)
        {
            this.IdDetalleVenta = idDetalle;
            this.FolioFactura = folioFactura;
            this.IdProducto = idProducto;
            this.PrecioUnidad = precioUnidad;
            this.Cantidad = cantidad;
        }
        private int _id;
        public int IdDetalleVenta
        {
            get { return _id; }
            set { _id = value; }

        }
        public int FolioFactura { get; set; }
        public string IdProducto { get; set; }

        private decimal _precioUnidad;
        public decimal PrecioUnidad
        {
            get { return _precioUnidad; }
            set { _precioUnidad = value; }
        }

        private int _cantidad;
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
    }
}
