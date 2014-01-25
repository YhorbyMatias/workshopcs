using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatos
{
    public class conexion
    {
        SqlConnection cn = null;

        public SqlConnection conectar()
        {
            cn = new SqlConnection("Data Source=EDWARD-PC\\SQLEXPRESS;Initial Catalog=LOGINS;Integrated Security=True");

            return cn;
        }
    }
}
