using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Factura
    {
        public Factura()
        {
            this.Detalles = new List<DetalleFactura>();
            this.ImpuestosAcumulados = 0M;
        }
        public Factura(int folio)
        {
            this.Folio= folio;
        }

        private decimal _impuestosAcumulados;
        public decimal ImpuestosAcumulados
        {
            get { return _impuestosAcumulados; }
            set { _impuestosAcumulados = value; }
        }

        private int _folio;
        public int Folio
        {
            get { return _folio; }
            set { _folio = value; }
        }

        public string IdCliente { get; set; }

        private DateTime _fecha;
        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string Direccion { get; set; }

        //public string Direccion { get; set; }

        private List<DetalleFactura> _detallesFactura;
        public List<DetalleFactura> Detalles
        {
            get { return _detallesFactura; }
            set { _detallesFactura = value; }

        }

        public decimal Total
        {
            get
            {
                //Lambda x es lo siguiente: x.PrecioUnidad * x.Cantidad
                if (Detalles.Count <= 0)
                    return 0;
                else
                    return this.Detalles.Sum(x => x.PrecioUnidad * x.Cantidad) + ImpuestosAcumulados;
            }
        }

        public string UserName { get; set; }
    }
}
