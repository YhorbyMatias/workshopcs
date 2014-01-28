using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;

namespace PuntoVenta
{
    internal static class Sesion
    {
        static Usuario _UsuarioActual = null;

        public static Usuario UsuarioActual
        {
            get { return Sesion._UsuarioActual; }
            set { Sesion._UsuarioActual = value; }
        }
    }
}
