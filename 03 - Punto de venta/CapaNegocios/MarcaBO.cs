using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;

namespace PuntoVenta.CapaNegocios
{
    public class MarcaBO
    {
        public static List<Marca> DevolverTodos()
        {
            return MarcaDAL.DevolverTodos();
        }


        public static int Crear(Marca _marca)
        {
            return MarcaDAL.Crear(_marca);
        }

        public static bool Modificar(Marca m)
        {
            return MarcaDAL.Modificar(m);
        }


        public static bool Eliminar(string id)
        {
            return MarcaDAL.Eliminar(id);
        }
    }
}
