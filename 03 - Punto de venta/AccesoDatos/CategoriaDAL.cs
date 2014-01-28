using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuntoVenta.Entidades;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace PuntoVenta.AccesoDatos
{
    public abstract class CategoriaDAL : AccesoDatos
    {
        //public static Categoria DevolverPorId(int id)
        //{
        //    Categoria categoria = new Categoria();
        //    using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
        //    {
        //        conn.Open();
        //        string varSql = @"SELECT IdCategoria,Descripcion FROM Categorias WHERE IdCategoria=@idcategoria";
        //        SqlCommand cmd = new SqlCommand(varSql, conn);
        //        cmd.Parameters.AddWithValue("@idcategoria", id);
        //        IDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //            categoria = CargarCategoria(reader);
        //    }
        //    return categoria;
        //}

        public static Categoria DevolverPorId(int id)
        {
            Categoria categoria = null;
            var listaCategorias = DevolverTodos();
            categoria = (from x in listaCategorias
                         where x.IdCategoria == id
                         select x).SingleOrDefault();
            return categoria;
        }

        public static List<Categoria> DevolverTodos()
        {
            List<Categoria> _categorias = null;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                _categorias = new List<Categoria>();
                string varSql = @"SELECT IdCategoria,Descripcion FROM Categorias";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    _categorias.Add(CargarCategoria(reader));
            }
            return _categorias;
        }

        public static int Crear(Categoria categoria)
        {
            //if (Existe(categoria))
            //{
            //    var listaCategorias = DevolverTodos();
            //    Categoria cat = new Categoria();
            //    cat = (from x in listaCategorias
            //           where (x.Descripcion.ToUpper().Equals(categoria.Descripcion.ToUpper()) || (x.IdCategoria == categoria.IdCategoria))
            //           select x).SingleOrDefault();
            //    categoria.IdCategoria = cat.IdCategoria;
            //    Actualizar(categoria);
            //    return categoria.IdCategoria;
            //}
            //PAra devolver el Id generado
            int resultado = 0;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                string varSql = @"INSERT INTO Categorias(Descripcion) VALUES (@descripcion);
                                SELECT SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                resultado = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return resultado;
        }

        //public static bool Existe(Categoria categoria)
        //{
        //    bool existe = false;
        //    using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
        //    {
        //        //El LIKE %valor% devuelve todos los registros que contengan el valor
        //        string varSql = string.Format(@"SELECT COUNT(*) FROM Categorias WHERE Descripcion LIKE %{0}%", categoria.Descripcion);
        //        SqlCommand cmd = new SqlCommand(varSql, conn);
        //        int resultado = 0;
        //        /*El executeNonQuery devuelve un entero con la cantidad de registros que afectó
        //         **/
        //        resultado = cmd.ExecuteNonQuery();
        //        //Entonces si resultado es mayor que cero es que existe :/
        //        existe = resultado > 0;
        //    }
        //    return existe;
        //}

        public static bool Existe(Categoria categoria)
        {
            bool existe = false;

            var listaCategorias = DevolverTodos();
            Categoria cat = (from x in listaCategorias
                             where (x.Descripcion.ToUpper().Equals(categoria.Descripcion.ToUpper()))
                             | (x.IdCategoria == categoria.IdCategoria) 
                             select x).SingleOrDefault();
            if (cat != null)
                existe = true;

            return existe;
        }

        public static bool Actualizar(Categoria categoria)
        {
            bool actualizado = false;
            using (SqlConnection conn = new SqlConnection(GlobalConnectionString))
            {
                conn.Open();
                int resultado = 0;
                string varSql = @"UPDATE Categorias SET Descripcion=@descripcion 
                                WHERE IdCategoria=@idcategoria";
                SqlCommand cmd = new SqlCommand(varSql, conn);
                cmd.Parameters.AddWithValue("@descripcion", categoria.Descripcion);
                cmd.Parameters.AddWithValue("@idcategoria", categoria.IdCategoria);
                resultado = cmd.ExecuteNonQuery();

                actualizado = resultado > 0;
            }
            return actualizado;
        }

        private static Categoria CargarCategoria(IDataReader reader)
        {
            Categoria categoria = null;
            categoria = new Categoria();
            categoria.IdCategoria = (int)reader["IdCategoria"];
            categoria.Descripcion = reader["Descripcion"].ToString();
            return categoria;
        }
    }
}
