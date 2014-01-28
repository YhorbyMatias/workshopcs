using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.AccesoDatos;
using PuntoVenta.Entidades;

namespace PuntoVenta.CapaNegocios
{
    public static class CategoriaBO
    {
        public static List<Categoria> DevolverTodos()
        {
            return CategoriaDAL.DevolverTodos();
        }

        public static Categoria DevolverPorId(int id)
        {
            return CategoriaDAL.DevolverPorId(id);
        }

        public static bool Existe(Categoria categoria)
        {
            return CategoriaDAL.Existe(categoria);
        }

        public static int Crear(Categoria categoria)
        {
            return CategoriaDAL.Crear(categoria);
        }

        public static bool Actualizar(Categoria categoria)
        {
            return CategoriaDAL.Actualizar(categoria);
        }
    }
}
