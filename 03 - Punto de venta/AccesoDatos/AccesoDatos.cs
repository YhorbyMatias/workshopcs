using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PuntoVenta.AccesoDatos
{
    public abstract class AccesoDatos
    {
        private static string _globalConnString;
        protected static string GlobalConnectionString
        {
            get
            {
                if (_globalConnString == null)
                {
                    _globalConnString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
                }
                if (_globalConnString == null)
                    throw new Exception("La cadena de conexión no está definida");

                return _globalConnString;
            }
        }
    }
}
