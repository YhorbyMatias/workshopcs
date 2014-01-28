using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;

namespace PuntoVenta.CapaNegocios
{
    public static class UnidadMedidaBO
    {

        public static List<UnidadMedida> DevolverTodos()
        {
            return UnidadMedidaDAL.DevolverTodos();
        }
    }
}
