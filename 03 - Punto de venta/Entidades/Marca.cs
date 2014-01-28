using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Marca
    {
        public int Id { get; set; }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
