using System.Collections.Generic;

namespace PuntoVenta.Entidades
{
    public class Proveedor
    {
        public string IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public List<Producto> Productos { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
