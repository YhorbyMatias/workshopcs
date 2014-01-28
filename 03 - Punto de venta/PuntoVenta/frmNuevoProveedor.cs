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
    public partial class frmNuevoProveedor : Form
    {
        public frmNuevoProveedor()
        {
            InitializeComponent();
        }

        public frmNuevoProveedor(Proveedor proveedorAEditar)
            : this()
        {
            ProveedorEditar = new Proveedor();
            ProveedorEditar = proveedorAEditar;
            modoEditar = true;
            txtId.Enabled = false;
        }
        bool modoEditar;

        public Proveedor ProveedorEditar { get; set; }

        public Proveedor ProveedorCreado { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmNuevoProveedor_Load(object sender, EventArgs e)
        {
            if (modoEditar)
            {
                txtId.Text = ProveedorEditar.IdProveedor;
                txtNombre.Text = ProveedorEditar.Nombre;
                txtTelefono.Text = ProveedorEditar.Telefono;
                txtEmail.Text = ProveedorEditar.Email;
                txtDireccion.Text = ProveedorEditar.Direccion;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (!Comprobar())
                return;

            DialogResult result = MessageBox.Show(this, "Esta seguro?", "Aviso", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
                return;

            try
            {
                if (modoEditar)
                {
                    Proveedor proveedoraaEditar = new Proveedor
{
    IdProveedor = txtId.Text,
    Telefono = txtTelefono.Text,
    Email = txtEmail.Text,
    Nombre = txtNombre.Text,
    Direccion = txtDireccion.Text
};
                    ProveedorBO.Actualizar(proveedoraaEditar);

                    MessageBox.Show("Hecho");
                    return;

                }

                Proveedor proveedoraCrear = new Proveedor
                {
                    IdProveedor = txtId.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text
                };

                ProveedorBO.Crear(proveedoraCrear);

                ProveedorCreado = proveedoraCrear;

                MessageBox.Show("Hecho");

                this.DialogResult = DialogResult.OK;

                this.Dispose();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }


        }


        bool Comprobar()
        {
            bool resultad = true;
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                errorProvider1.SetError(txtId, "Escriba un ID");
                resultad = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider1.SetError(txtNombre,"Escriba el nombre del proveedor");

                resultad = false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                errorProvider1.SetError(txtTelefono, "Escriba un telefono");
                resultad = false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Escriba un email");
                resultad = false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                errorProvider1.SetError(txtDireccion, "Escriba una direccion");
                resultad = false;
            }




            return resultad;
        }
    }
}
