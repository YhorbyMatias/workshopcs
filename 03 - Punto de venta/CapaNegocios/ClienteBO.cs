using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;
namespace PuntoVenta.CapaNegocios
{
    public class ClienteBO
    {
        public static List<Cliente> DevolverTodos()
        {
            return ClienteDAL.DevolverTodos();
        }

        public static Cliente BuscarPorId(string id)
        {
            return ClienteDAL.BuscarPorId(id);
        }

        public static bool Eliminar(string id)
        {
            return ClienteDAL.Eliminar(id);
        }

        public static string Crear(Cliente cliente)
        {
            if ((ClienteDAL.Existe(cliente.Id)))
            {
                throw new Exception("El Id del cliente existe");
            }
            else
            {
                ClienteDAL.Crear(cliente);
            }
            return cliente.Id;
        }

        public static bool Actualizar(Cliente clienteAActualizar)
        {
            return ClienteDAL.Actualizar(clienteAActualizar);
        }

    }
}
