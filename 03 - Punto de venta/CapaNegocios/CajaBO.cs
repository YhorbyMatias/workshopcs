using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;

namespace PuntoVenta.CapaNegocios
{
    public static class CajaBO
    {
        public static int Crear(Caja caja)
        {
            return CajaDAL.Crear(caja);
        }


        public static bool AbrirCaja(int id)
        {
            return CajaDAL.EstablecerEstado(id, true);
        }

        public static bool CerrarCaja(int id)
        {
            return CajaDAL.EstablecerEstado(id, false);
        }

    }
}
