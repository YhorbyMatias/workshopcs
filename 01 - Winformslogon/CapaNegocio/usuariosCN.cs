using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;
using System.Data;

namespace CapaNegocio
{
    public class usuariosCN
    {
        usuariosDAO usuariosdao = new usuariosDAO();

        public DataSet ListaUsuarios()
        {
            return usuariosdao.ListaUsuarios();
        }

        public int Ultimocodigousuarios()
        {
            return usuariosdao.Ultimocodigousuarios();
        }

        public String logear(ceUsuarios obUsuarios)
        {
            return usuariosdao.logear(obUsuarios);
        }

        public String RegistrarUsuarios(ceUsuarios obUsuarios)
        {
            return usuariosdao.RegistrarUsuarios(obUsuarios);
        }

        public String ModificarUsuarios(ceUsuarios obUsuarios)
        {
            return usuariosdao.ModificarUsuarios(obUsuarios);
        }

        public String Conocer_Rol(ceUsuarios obUsuarios)
        {
            return usuariosdao.Conocer_Rol(obUsuarios);
        }

       

        
        

        

        
    }
}
