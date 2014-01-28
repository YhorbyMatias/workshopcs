using System.Collections.Generic;
using PuntoVenta.AccesoDatos;
using PuntoVenta.Entidades;
using System;

namespace PuntoVenta.CapaNegocios
{
    public static class ProductoBO
    {
        public static List<Producto> DevolverTodos()
        {
            return ProductoDAL.DevolverTodos();
        }

        public static bool Crear(Producto pro)
        {
            if (pro == null)
                throw new Exception("El producto no está inicializado");
            return ProductoDAL.Crear(pro);
        }

        public static Producto BuscarPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new Exception("Tiene que poner un ID válido");

            return ProductoDAL.BuscarPorId(id);
        }

        public static decimal GetPrecioPorIdProducto(string id)
        {
            return ProductoDAL.GetPrecioPorIdProducto(id);
        }

        public static bool Actualizar(Producto pro)
        {
            return ProductoDAL.Actualizar(pro);
        }

        public static bool Eliminar(string idProductoAEliminar, string usuarioResponsable = "")
        {
            return ProductoDAL.Eliminar(idProductoAEliminar, usuarioResponsable);
        }

        public static void AgregarExistencias(string idProducto,int existenciasParaAgregar,string usuarioResponsable)
        {

            if (existenciasParaAgregar < 0)
                throw new Exception("No puede agregar existencias negativas");

            if (existenciasParaAgregar == 0)//si manda a agregar  0 existencias no hacer nada (terminar)
                return;

            if (string.IsNullOrWhiteSpace(usuarioResponsable))
                throw new Exception("Debe haber un usuario responsable");

            if (string.IsNullOrWhiteSpace(idProducto))
                throw new Exception("Debe de haber un IdProducto válido");

                ProductoDAL.AgregarExistencia(idProducto, existenciasParaAgregar, usuarioResponsable);
            
        }

        public static void ActualizarExistencia(string idProducto, int nuevaExistencia)
        {
            if (string.IsNullOrWhiteSpace(idProducto))
                throw new Exception("Debe proveer un Id de producto válido");

            if (nuevaExistencia < 0)
                throw new Exception("No se puede actualizar las existencias con un número negativo");

            ProductoDAL.ActualizarExistencia(idProducto, nuevaExistencia);
        }
    }
}
