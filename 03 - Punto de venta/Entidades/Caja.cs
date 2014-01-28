using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Caja
    {
        public int IdCaja { get; set; }
        public string Descripcion { get; set; }
        public bool Disponible { get; set; }
        public Usuario Usuario { get; set; }

        public override string ToString()
        {
            return string.Format("Caja #{0}, Usuario: {1}", IdCaja, Usuario.UserName);
        }
    }
}
