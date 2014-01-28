using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class UnidadMedida
    {
        public int IdUnidadMedida { get; set; }
        public string Descripcion { get; set; }

        public UnidadMedida()
        {                
        }

        public UnidadMedida(string desc)
        {
            this.Descripcion = desc;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
