using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Webforms_SQL_Query
{
    //Esta Clase llama un store procedure para que inserte en la BD
    public class Inserta
    {

        private SqlConnection con =
            new SqlConnection("Data Source=Root-PC;Initial Catalog=Prueba;Integrated Security=True");

        public Int32 guarda(string nombre, string ahorro)
        {

            try
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("inserta_nombre_y_ahorro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@ahorro", ahorro);

                return cmd.ExecuteNonQuery();

            }
            catch (SqlException)
            {
                return 0;
            }

        }
    }
}

