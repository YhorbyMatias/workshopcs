using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.Entidades;
using PuntoVenta.CapaNegocios;

namespace PuntoVenta
{
    public partial class frmCambiarCantidadProducto : Form
    {
        public frmCambiarCantidadProducto()
        {
            InitializeComponent();
        }

        public frmCambiarCantidadProducto(string idProducto,int cantidadActual):this()
        {
            this.idProducto = idProducto;


            this.cantidadActual = cantidadActual;
        }
        public delegate void CantidadNuevaEventHandler(string idProducto, int cantidadNueva);

        public event CantidadNuevaEventHandler AlSeleccionarCantidad;


        //id del producto a cambiar la cantidad
        string idProducto;

        //cantidad actual el el frmVenta
        int cantidadActual;

        Producto producto;

        private void frmCambiarCantidadProducto_Load(object sender, EventArgs e)
        {
            producto = ProductoBO.BuscarPorId(idProducto);

            lblNombreProducto.Text = producto.Nombre;

            txtCantidadAlmacen.Text = producto.Cantidad.ToString();

            txtCantidadActual.Text = cantidadActual.ToString();

            nudNuevaCantidad.Maximum = producto.Cantidad;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Comprobar())
                return;

            if (MessageBox.Show(this, "Estás seguro?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                if (AlSeleccionarCantidad != null)
                    AlSeleccionarCantidad(idProducto, Convert.ToInt32(nudNuevaCantidad.Value));

                this.Dispose();
            }
        }


        bool Comprobar()
        {
            bool resultado = true;

            if (nudNuevaCantidad.Value > producto.Cantidad)
            {
                resultado = false;
                MessageBox.Show("Ha seleccionado más cantidad de las que existen en el almacén");
            }

            if(nudNuevaCantidad.Value<=0)
            {
                resultado=false;
                MessageBox.Show("Ha seleccionado una cantidad inválida");
            }
            return resultado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
