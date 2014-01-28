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
    public partial class frmSeleccionarProducto : Form
    {
        public frmSeleccionarProducto()
        {
            InitializeComponent();
        }
        //Delegado y evento que manejan cuando se ha seleccionado un producto
        public delegate void SeleccionadoEventHandler(string idProductoSeleccionada, int cantidadSeleccionada);
        public event SeleccionadoEventHandler ProductoSeleccionado;

        //Propiedad que guarda los productos para ser mostrarlos en el ComboBox
        private List<Producto> _listaproductos;

        public List<Producto> ListaProductos
        {
            get { return _listaproductos; }
            set { _listaproductos = value; }
        }

        private void frmSeleccionarProducto_Load(object sender, EventArgs e)
        {
            ActualizarComboBox();

            //Asigno aquí el manejador de eventos al evento selectvalueChanged
            //porque si se dejara la forma predeterminada tiraría una excepción
            this.comboProducto.SelectedValueChanged += new System.EventHandler(this.comboProducto_SelectedValueChanged);
            
            //Llamo al manejador de eventos para mostrar el precio del producto
            //que está primero en el comboBox
            comboProducto_SelectedValueChanged(comboProducto, null);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Si la comprobación no fue satisfactoria, detener
                if (!Comprobar())
                    return;

                //guardo el id del producto seleccionado del comboBox
                string idProductoSeleccionado = comboProducto.SelectedValue.ToString();

                //guardar la cantidad seleccionada del nudCantidad
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                //si el evento productoSeleccionado no es null (tiene suscriptores)
                if (ProductoSeleccionado != null)
                {
                    //ejecutar el evento
                    ProductoSeleccionado(idProductoSeleccionado, cantidad);

                    this.DialogResult = DialogResult.OK;

                    //cerrar
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        void ActualizarComboBox()
        {
            comboProducto.DataSource = null;
            //Llenando el comboBox con todos los productos
            comboProducto.ValueMember = "IdProducto";
            comboProducto.DisplayMember = "Nombre";
            ListaProductos = ProductoBO.DevolverTodos();
            comboProducto.DataSource = ListaProductos;
        }
        bool Comprobar()
        {
            bool resultado = true;
            if (nudCantidad.Value <= 0)
            {
                MessageBox.Show("Seleccione una cantidad mayor a 0");
                resultado = false;
            }
            Producto productoActual = (from x in ListaProductos
                                       where x.IdProducto == comboProducto.SelectedValue.ToString()
                                       select x).SingleOrDefault();
            if (nudCantidad.Value > productoActual.Cantidad)
            {
                MessageBox.Show("Ha seleccionado una cantidad mayor a la disponible del producto");
                resultado = false;
            }
            return resultado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void comboProducto_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender == comboProducto)
            {
                Producto productoSeleccionado = ProductoBO.BuscarPorId(((ComboBox)sender).SelectedValue.ToString());
                if (productoSeleccionado.Cantidad == 0)
                {
                    MessageBox.Show(
                                 string.Format("El producto: Código: {0} Nombre: {1} .\nNo tiene existencias en el almacen", productoSeleccionado.IdProducto, productoSeleccionado.Nombre)
                                    );
                    nudCantidad.Enabled = false;
                }
                else if (productoSeleccionado.Cantidad <= 10)
                {
                    MessageBox.Show(
                        string.Format(@"Por favor comunicarse con el proveedor del producto de Código: {0} \nNombre: {1} ya que se están agotando las existencias. Quedan {2} existencias.", productoSeleccionado.IdProducto, productoSeleccionado.Nombre, productoSeleccionado.Cantidad)
                                     );
                    nudCantidad.Enabled = true;
                }
                else
                {
                    nudCantidad.Enabled = true;
                }
                lblPrecio.Text = productoSeleccionado.PrecioVenta.ToString();
                nudCantidad.Maximum = productoSeleccionado.Cantidad;
            }
        }
    }
}
