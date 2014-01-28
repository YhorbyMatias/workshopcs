using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }

        public Categoria()
        {

        }

        public Categoria(string desc)
        {
            this.Descripcion = desc;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
