using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using CapaEntidad;
using System.Data;


namespace CapaDatos
{
    public class usuariosDAO
    {
        conexion conexion = new conexion();
        SqlDataAdapter da = null;
        SqlConnection cn = null;
        /*voya declare un datatable  es similar a  un datareader
         utilizo mas datatable pero el resultao es lo mismo*/
        DataTable dt;

        public DataSet ListaUsuarios()
        {
            DataSet ds = new DataSet();
            cn = conexion.conectar();
            da = new SqlDataAdapter("sp_ListaUSUARIOS1", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(ds, "usuarios");
            da.Dispose();
            cn.Dispose();
            return ds;
        }

        public String logear(ceUsuarios obUsuarios)
        {
            cn = conexion.conectar();
           
            String resultado = "y";
            
            try
            {
                da = new SqlDataAdapter("sp_USUARIOSS", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = obUsuarios.Usuario;
                da.SelectCommand.Parameters.Add("@clave", SqlDbType.NVarChar).Value = obUsuarios.Clave;
                cn.Open();
                dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    resultado = "ingresar datos correctos";
                }
                else {
                    /*en esta linea capturar el nombre de usuario*/
                    String nomusu = dt.Rows[0][0].ToString();
                    resultado = "Bienvenido:" + nomusu;
                }
                return resultado; 
            }
            catch (Exception e)
            {
                return "Error:" + e.Message;
            }
            finally
            { 
                da.Dispose();
                conexion.conectar().Dispose();
            }
        }
        public String RegistrarUsuarios(ceUsuarios obUsuarios)
        {
            cn = conexion.conectar();
            try
            {
                da = new SqlDataAdapter("sp_registrodeusuarios", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = obUsuarios.Usuario;
                da.SelectCommand.Parameters.Add("@clave", SqlDbType.NVarChar).Value = obUsuarios.Clave;
                da.SelectCommand.Parameters.Add("@estado", SqlDbType.NVarChar).Value = obUsuarios.Estado;
                da.SelectCommand.Parameters.Add("@rol", SqlDbType.NVarChar).Value = obUsuarios.Rol;
                cn.Open();
                da.SelectCommand.ExecuteNonQuery();
                return "Registro Correcto";
            
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
            finally
            {
                da.Dispose();
                conexion.conectar().Dispose();
            }
        }
        public String ModificarUsuarios(ceUsuarios obUsuarios)
        {
            cn = conexion.conectar();
            try
            {
                da = new SqlDataAdapter("sp_ActualizarUsuarios", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = obUsuarios.Usuario;
                da.SelectCommand.Parameters.Add("@clave", SqlDbType.NVarChar).Value = obUsuarios.Clave;
                da.SelectCommand.Parameters.Add("@estado", SqlDbType.NVarChar).Value = obUsuarios.Estado;
                da.SelectCommand.Parameters.Add("@rol", SqlDbType.NVarChar).Value = obUsuarios.Rol;
                cn.Open();
                da.SelectCommand.ExecuteNonQuery();
                return "Modificado Correcto";

            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
            finally
            {
                da.Dispose();
                conexion.conectar().Dispose();
            }
        }

        public int Ultimocodigousuarios()
        {
            cn = conexion.conectar();
            da = new SqlDataAdapter("sp_Ultimocodigousuarios", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            cn.Open();
            int numero = int.Parse(da.SelectCommand.ExecuteScalar().ToString()) + 1;

            return numero;
        }

        public String Conocer_Rol(ceUsuarios obUsuarios)
        {
            cn = conexion.conectar();
            da = new SqlDataAdapter("sp_rol", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = obUsuarios.Usuario;
            cn.Open();
            String rol = (da.SelectCommand.ExecuteScalar().ToString());

            return rol;
        }
        
        
    }
}
