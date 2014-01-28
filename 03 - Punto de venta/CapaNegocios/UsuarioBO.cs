using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using PuntoVenta.AccesoDatos;

namespace PuntoVenta.CapaNegocios
{
    public class UsuarioBO
    {
        public static List<Usuario> DevolverTodos()
        {
            return UsuarioDAL.DevolverTodos();
        }

        public static bool Modificar(Usuario usuario)
        {
            return UsuarioDAL.Modificar(usuario);
        }

        public static bool Comprobar(string userName, string contraseña)
        {
            return UsuarioDAL.Comprobar(userName, contraseña);
        }

        public static Usuario DevolverPorID(string userName)
        {
            return UsuarioDAL.DevolverPorId(userName);
        }

        public static bool CrearUsuario(Usuario _usuario)
        {
            return UsuarioDAL.Crear(_usuario);
        }

        public static bool EliminarUsuario(string userName, string usuarioActual,string comentarios)
        {
            return UsuarioDAL.EliminarUsuario(userName,usuarioActual,comentarios);
        }
    }
}
