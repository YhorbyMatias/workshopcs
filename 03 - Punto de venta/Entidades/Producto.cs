using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuntoVenta.Entidades
{
    public class Producto
    {

        public string IdProducto { get; set; }
        public string Nombre { get; set; }
        public Marca Marca { get; set; }
        public UnidadMedida UnidadMedida { get; set; }

        private int _cantidad;
        public int Cantidad
        {
            get { return _cantidad; }
            set
            {
                _cantidad = (value < 0) ? 0 : value;
            }
        }
        private decimal _precioCompra;
        public decimal PrecioCompra
        {
            get { return _precioCompra; }
            set { _precioCompra = (value < 0) ? 0M : value; }
        }
        private decimal _precioVenta;
        public decimal PrecioVenta
        {
            get { return _precioVenta; }
            set { _precioVenta = (value < 0) ? 0M : value; }
        }
        public Categoria Categoria { get; set; }
        public Proveedor Proveedor { get; set; }

        private int _cantidadMayoreo;
        public int CantidadMayoreo
        {
            get { return _cantidadMayoreo; }
            set { _cantidadMayoreo = (value < 0) ? 0 : value; }
        }

        public string Foto { get; set; }
        public string Descripcion { get; set; }

        public Producto(string id, string nombre)
        {
            this.IdProducto = id;
            this.Nombre = nombre;
        }
        public Producto():this(null,null)
        {
        }
        public Producto(string id,string nombre,Marca marca,decimal precioVenta,decimal precioCompra):this(id,nombre)
        {
            this.Marca = marca;
            this.PrecioVenta = precioVenta;
            this.PrecioCompra = precioCompra;
        }

        /// <summary>
        /// Propiedad que controla el usuario que registró el producto
        /// en la base de datos
        /// </summary>
        private Usuario _usuarioResponsable;
        private Usuario UsuarioResponsable
        {
            get { return _usuarioResponsable; }
            set { _usuarioResponsable = value; }
        }

        /// <summary>
        /// Devuelve el usuario que registró el producto en la base de datos
        /// </summary>
        /// <returns>Usuario</returns>
        public Usuario GetUsuarioResponsable()
        {
            return UsuarioResponsable;
        }
        /// <summary>
        /// Establecer el usuario que registró el producto en la bd
        /// </summary>
        /// <param name="usuario">Usuario que creó el producto en la base de datos</param>
        public void SetUsuarioResponsable(Usuario usuario)
        {
            UsuarioResponsable = usuario;
        }


        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }



}
