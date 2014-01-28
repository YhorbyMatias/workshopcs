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
    public partial class frmExistencias : Form
    {
        /// <summary>
        /// Constructor predeterminado cuando se va a agregar existencias al almacén
        /// </summary>
        public frmExistencias()
        {
            InitializeComponent();
            this.Text = "Agregar existencias al almacén";
            this.label1.Text = "Cantidad a agregar:";
        }
        /// <summary>
        /// Constructor para cuando se va a corregir las existencias
        /// </summary>
        /// <param name="corregir"></param>
        public frmExistencias(bool corregir)
        {
            InitializeComponent();
            this.Text = "Corregir existencias";
            this.label1.Text = "Nueva cantidad";
            ActualizarExistencias = true;
        }

        /// <summary>
        /// Propiedad que controla si el formulario se llamó para 
        /// modo de corregir existencias
        /// </summary>
        private bool _modoActualizarExistencias;

        private bool ActualizarExistencias
        {
            get { return _modoActualizarExistencias; }
            set { _modoActualizarExistencias = value; }
        }

        /// <summary>
        /// Propiedad que almacena todos los productos devueltos
        /// </summary>
        private List<Producto> _productos;

        public List<Producto> Productos
        {
            get { return _productos; }
            set { _productos = value; }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //Guardo los datos del producto que tenga el id que concuerde  
                //con el seleccionado en el comboBox
                Producto productoAProcesar = (from x in Productos
                                              where x.IdProducto == cboProductos.SelectedValue.ToString()
                                              select x).SingleOrDefault();

                if (ActualizarExistencias) //compruebo si está en el modo de actualizar existencias
                {
                    StringBuilder mensaje = new StringBuilder();
                    mensaje.AppendFormat(@"¿Estás seguro de que desea actualizar la cantidad en existencia del producto: {0}?\n
                                       Existencia actual: ({1}), Nueva existencia: ({2})", productoAProcesar.Nombre, productoAProcesar.Cantidad, nudExistencia.Value.ToString());

                    //mostrar un cuadro de diálogo para la confirmación
                    DialogResult resultado = MessageBox.Show(this, mensaje.ToString(), "Aviso",
                                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2);

                    if (resultado == DialogResult.Yes) //Sí se seleccionó que SÍ entonces...
                    {
                        ProductoBO.ActualizarExistencia(cboProductos.SelectedValue.ToString(), (int)nudExistencia.Value);
                        MessageBox.Show("Hecho");
                    }
                }
                else
                {

                    StringBuilder mensaje = new StringBuilder();
                    mensaje.AppendFormat(@"¿Estás seguro de que desea agregar a las existencias del producto: {0}?\n
                                       Existencia actual: ({1}), Nueva existencia: ({2})", productoAProcesar.Nombre, productoAProcesar.Cantidad, (productoAProcesar.Cantidad + nudExistencia.Value).ToString());

                    //Mostrar el cuadro de diálogo con la confirmación
                    DialogResult resultado = MessageBox.Show(this, mensaje.ToString(), "Aviso",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                             MessageBoxDefaultButton.Button2);


                    if (resultado == DialogResult.Yes) //Si se seleccionó que sí
                    {
                        ProductoBO.AgregarExistencias(cboProductos.SelectedValue.ToString(), (int)nudExistencia.Value, Sesion.UsuarioActual.UserName);

                        MessageBox.Show("Hecho");

                        this.Dispose();
                    }
                }

                //CargarProductos();
                //cboProductos_SelectionChangeCommitted(null, null);
                //nudExistencia.Value = 0M;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void frmExistencias_Load(object sender, EventArgs e)
        {
            CargarProductos();
            cboProductos_SelectionChangeCommitted(null,null);
        }




        void CargarProductos()
        {
            Productos = null;
            cboProductos.DataSource=null;
            Productos = new List<Producto>();
            Productos = ProductoBO.DevolverTodos();
            cboProductos.DataSource = Productos;
            cboProductos.DisplayMember = "Nombre";
            cboProductos.ValueMember = "IdProducto";
        }

        void ActualizarDatos()
        {
            Producto productoComboBox = new Producto();
            productoComboBox = (from x in Productos
                                where x.IdProducto == cboProductos.SelectedValue.ToString()
                                select x).SingleOrDefault();
                label2.Text = productoComboBox.IdProducto;
                label3.Text = productoComboBox.Marca.Descripcion;
                label4.Text = productoComboBox.Proveedor.Nombre;
                label5.Text = productoComboBox.Cantidad.ToString();
        }

        private void cboProductos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActualizarDatos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
