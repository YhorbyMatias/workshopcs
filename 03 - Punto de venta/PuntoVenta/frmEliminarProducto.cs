using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.CapaNegocios;
using PuntoVenta.Entidades;

namespace PuntoVenta
{
    public partial class frmEliminarProducto : Form
    {
        public frmEliminarProducto()
        {
            InitializeComponent();
            _productos = new List<Producto>();
        }
        //Propiedad para controlar el DATASOURCE del comboBox que 
        //guarda los productos a eliminar
        List<Producto> _productos;

        private List<Producto> Productos
        {
            get { return _productos; }
            set { _productos = value; }
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void frmEliminarProducto_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Producto productoAEliminar = (from x in Productos
                                          where x.IdProducto == cboProductos.SelectedValue.ToString()
                                          select x).SingleOrDefault();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("¿Estás seguro de que desea eliminar el siguiente producto:");
            sb.AppendFormat("{0}: {1} - ({2}), {3}: {4}", "Producto", productoAEliminar.Nombre, productoAEliminar.Marca,
                                "Proveedor", productoAEliminar.Proveedor);
            DialogResult respuesta = MessageBox.Show(this, sb.ToString(), "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    ProductoBO.Eliminar(productoAEliminar.IdProducto, Sesion.UsuarioActual.UserName);
                    MessageBox.Show(this, "Producto eliminado satisfactoriamente", "Eliminar producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProductos();
                }
                catch (Exception excepcion)
                {
                    MessageBox.Show(excepcion.Message);
                }
                finally
                {
                    productoAEliminar = null;
                    sb = null;
                }
            }

        }


        void CargarProductos()
        {
            cboProductos.DataSource = null;
            cboProductos.Items.Clear();
            Productos = ProductoBO.DevolverTodos();
            cboProductos.DataSource = Productos;
            cboProductos.DisplayMember = "Nombre";
            cboProductos.ValueMember = "IdProducto";
        }


    }
}
