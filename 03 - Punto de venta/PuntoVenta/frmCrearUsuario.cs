using System;
using System.Windows.Forms;
using PuntoVenta.CapaNegocios;
using PuntoVenta.Entidades;

namespace PuntoVenta
{
    public partial class frmCrearUsuario : Form
    {
        public frmCrearUsuario()
        {
            InitializeComponent();
        }
        private void frmCrearUsuario_Load(object sender, EventArgs e)
        {
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            error.Clear();
            if (ComprobacionControles())
            {
                if (ComprobarNombreUsuario(txtUserName.Text))
                    MessageBox.Show("El usuario existe, elija otro nombre de usuario");
                else
                {
                    DialogResult resultado = MessageBox.Show(this,"¿Estás seguro?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                    if(resultado==DialogResult.Yes)
                        GuardarUsuario();
                }
            }
        }

        bool ComprobacionControles()
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                error.SetError(txtUserName, "No deje el campo nombre de usuario vacío");
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                error.SetError(txtContraseña, "Escriba una contraseña, no la deje vacía");
                resultado = false;
            }
            if (!txtContraseña.Text.Equals(txtRepetirContraseña.Text))
            {
                resultado = false;
                String mensaje="Las contraseñas no coinciden";
                error.SetError(txtContraseña, mensaje);
                error.SetError(txtRepetirContraseña, mensaje);
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                error.SetError(txtNombre, "Escriba el nombre real del usuario");
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                error.SetError(txtApellido, "Escriba el apellido del usuario");
                resultado = false;
            }
            return resultado;
        }



        bool ComprobarNombreUsuario(string nombre)
        {
            bool Existe=true;
            Usuario _usuario = UsuarioBO.DevolverPorID(nombre);
            if (_usuario == null)
                Existe = false;
            return Existe;
        }

        void GuardarUsuario()
        {
            Usuario _usuario = new Usuario();
            _usuario.UserName = txtUserName.Text;
            _usuario.Contraseña = txtContraseña.Text;
            _usuario.Nombre = txtNombre.Text;
            _usuario.Apellido = txtApellido.Text;
            _usuario.Reportes = chkReportes.Checked;
            _usuario.Vender = chkVender.Checked;
            _usuario.Administrar = chkAdministrar.Checked;
            _usuario.DeshacerVenta = chkDeshacerVenta.Checked;
            _usuario.Consultar = chkConsultas.Checked;
            _usuario.Catalogos = chkCatalogos.Checked;
            if (UsuarioBO.CrearUsuario(_usuario))
            {
                MessageBox.Show("Usuario creado");
                this.Dispose();
            }
            else
                MessageBox.Show("Error");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

}

