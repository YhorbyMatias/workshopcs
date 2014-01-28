using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Provincia { get; set; }

        //Esto es para no estar con el .Nombre a cada rato
        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
