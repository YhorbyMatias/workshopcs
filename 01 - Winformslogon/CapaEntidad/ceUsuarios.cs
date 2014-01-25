using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CapaEntidad
{
    public class ceUsuarios
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private String _usuario;

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        private String _clave;

        public String Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }
        private String _estado;

        public String Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        private String _rol;

        public String Rol
        {
            get { return _rol; }
            set { _rol = value; }
        }
    }
}
