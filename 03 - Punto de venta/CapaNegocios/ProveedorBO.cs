using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;

namespace PuntoVenta.CapaNegocios
{
    public static class ProveedorBO
    {
        public static List<Proveedor> DevolverTodos()
        {
            return ProveedorDAL.DevolverTodos();
        }

        public static Proveedor DevolverPorId(string id)
        {
            return ProveedorDAL.DevolverPorId(id);
        }

        public static void Crear(Proveedor proveedorACrear)
        {
            ProveedorDAL.Crear(proveedorACrear);
        }

        public static void Actualizar(Proveedor pro)
        {
            ProveedorDAL.Actualizar(pro);
        }
    }
}
