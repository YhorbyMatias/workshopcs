using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Usuario
    {
        public string UserName { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }


        public bool Vender { get; set; }
        public bool Reportes { get; set; }
        public bool Administrar { get; set; }
        public bool Consultar { get; set; }
        public bool DeshacerVenta { get; set; }
        public bool Catalogos { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}
