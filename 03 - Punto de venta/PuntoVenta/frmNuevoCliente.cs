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
    public partial class frmNuevoCliente : Form
    {
        public frmNuevoCliente()
        {
            InitializeComponent();
            CancelButton = btnCancel;
            _clientParam = new Cliente();
        }

        public frmNuevoCliente(Cliente paramCliente)
        {
            InitializeComponent();
            CancelButton = btnCancel;
            _clientParam = paramCliente;

            //Pongo la variable modoEditar en true porque se está modificando un cliente
            _modoEditar = true;

            //Poner el txtCodigo como readOnly porque sólo se va a modificar los datos
            //de un cliente que existe
            txtCodigo.ReadOnly = true;
            //Desactivar el chkGenerarCódigo porque si se activa
            //uede haber problemas al poderse genera el código
            chkGenerarCodigo.Checked = false;
            chkGenerarCodigo.Enabled = false;
        }

        //Esta variable es para controlar si el formulario se llamó
        //para crear un nuevo cliente (false) o para editar uno (true)
        bool _modoEditar = false;

        Cliente _clientParam;//Aqui guardo el parametro pasado cuando se va a editar un cliente


        string idClienteCreado;

        public string IdClienteCreado
        {
            get { return idClienteCreado; }
            set { idClienteCreado = value; }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
                bool controlesOk = ComprobarControles();//guarda el resultado de comprobar los controles

                if (controlesOk)//si los controles fueron comprobados sin fallos
                {
                    errorProvider1.Clear();//borro los avisos rojos de los errores que tanto joden

                    //Aquí guardo la opción que eligió el usuario 
                    DialogResult resultado = MessageBox.Show("¿Está seguró de que desea proceder?", "Aviso", MessageBoxButtons.YesNo);
                    
                    if (resultado == DialogResult.Yes)//Si se eligió que sí
                    {
                        IdClienteCreado = CrearCliente(); //creo el cliente
                        this.DialogResult = DialogResult.OK;
                    }//fin del if de comprobación del DialogResult

                }//fin del if de comprobación de controles

           
        }//fin del manejador de evento click del botón Ok


        string CrearCliente()
        {
            /*ESTE MÉTODO DEVUELVE EL ID DEL CLIENTE NUEVO
             * */
            Cliente cliente = new Cliente();

            if (chkGenerarCodigo.Checked)//si se eligió el checkbox de generar código entonces...
                GenerarId();//se genera uno aleatoriamente

            //asigno los datos a las propiedades del cliente
            cliente.Id = txtCodigo.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.Cedula = txtCedula.Text;
            cliente.Email = txtEmail.Text;
            cliente.Provincia = txtProvincia.Text;
            cliente.Direccion =txtDireccion.Text;
            try
            {
                if (_modoEditar)//si modoEditar es true se actualiza
                {
                    if (ClienteBO.Actualizar(cliente))
                        MessageBox.Show("Cliente actualizado");
                    else
                        MessageBox.Show("No se pudo actualizar");
                }
                else //sino se crea un cliente nuevo
                {
                    IdClienteCreado=ClienteBO.Crear(cliente);
                    MessageBox.Show("Cliente Creado");
                }


                this.DialogResult = DialogResult.OK;//poner que todo fue Ok
                this.Dispose(true); //cerrar y liberar recursos
            }
            catch (Exception excepcion)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(excepcion.Message);
                sb.AppendLine(excepcion.StackTrace);
                MessageBox.Show(this, sb.ToString(), "Excepción lanzada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return cliente.Id;//devolver id del cliente
        }
        

        bool ComprobarControles()
        {
            bool resultado = true;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre, "Escriba un nombre");
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                errorProvider1.SetError(txtApellido, "Escriba un apellido");
                resultado = false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                resultado = false;
                errorProvider1.SetError(txtDireccion, "Debe especificar una direccion");
            }
            if (string.IsNullOrWhiteSpace(txtProvincia.Text))
            {
                resultado = false;
                errorProvider1.SetError(txtProvincia, "Escriba una provincia");
            }
            //Si chkCedula no está seleccionado y la cédula no contiene los caracteres necesarios
            if (!chkCedula.Checked && txtCedula.Text.Count() != 13)
            {
                errorProvider1.SetError(txtCedula, @"Escriba una cédula válida, si no tiene cédula entonces\n
                                                    seleccione la opción 'No tiene'");
                resultado = false;
            }

            if (!chkGenerarCodigo.Checked && string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                errorProvider1.SetError(txtCodigo, "Escriba un código o seleccione 'Generar' para crear uno aleatoriamente'");
                resultado = false;
            }
            return resultado;
        }

        private void frmNuevoCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        void GenerarId()
        {
            int cantidad = txtNombre.Text.Count();
            StringBuilder sb = new StringBuilder();
            sb.Append(txtNombre.Text.Remove(cantidad / 2, cantidad - cantidad / 2));
            Random rnd = new Random(DateTime.Now.Millisecond);
            int numAleatorio = rnd.Next(DateTime.Now.Millisecond);
            sb.Append(numAleatorio);
            txtCodigo.Text = sb.ToString();
        }

        void CargarDatos()
        {
            txtCodigo.Text = _clientParam.Id;
            txtNombre.Text = _clientParam.Nombre;
            txtApellido.Text = _clientParam.Apellido;
            txtCedula.Text = _clientParam.Cedula;
            txtEmail.Text = _clientParam.Email;
            txtDireccion.Text = _clientParam.Direccion;
            txtProvincia.Text = _clientParam.Provincia;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == chkGenerarCodigo)
                txtCodigo.ReadOnly = txtCodigo.ReadOnly ^ true;
            else if (sender == chkCedula)
                txtCedula.ReadOnly = txtCedula.ReadOnly ^ true;
        }


    }
}
