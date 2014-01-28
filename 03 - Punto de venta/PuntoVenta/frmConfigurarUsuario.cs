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
    public partial class frmConfigurarUsuario : Form
    {
        public frmConfigurarUsuario()
        {
            InitializeComponent();
        }
        Usuario _usuario = null;//Variable para guardar el Usuario seleccionado en el comboBox
        List<Usuario> _usuarios = null;//Lista de usuarios para llenar el comboBox
        private void frmConfigurarUsuario_Load(object sender, EventArgs e)
        {
            _usuarios = UsuarioBO.DevolverTodos();//llenando la lista
            cboUsuarios.DataSource = _usuarios;//poniendo la lista como fuente de datos a comboBox
            cboUsuarios.ValueMember = "UserName";
            cboUsuarios.DisplayMember = "Usuario";
            //Aqui voy a hacer que el valor del comboBox seleccionado al entrar al formulario
            //sea el usuario Actual
            tabControl1.Enabled = false;
            foreach (Usuario valor in cboUsuarios.Items)
            {
                if (valor.UserName == Sesion.UsuarioActual.UserName)
                {
                    cboUsuarios.SelectedItem = valor;
                    _usuario = UsuarioBO.DevolverPorID(Sesion.UsuarioActual.UserName);
                }
            }
        }

        private void cboUsuarios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            frmConfirmarUsuario fconfirmar = new frmConfirmarUsuario(cboUsuarios.SelectedValue.ToString());
            fconfirmar.AlConfirmar += new frmConfirmarUsuario.AlConfirmarEventHandler(fconfirmar_AlConfirmar);
            fconfirmar.ShowDialog(this);
        }

        void fconfirmar_AlConfirmar(bool valor)
        {
            if (valor)
            {
                tabControl1.Enabled = true;
                _usuario = UsuarioBO.DevolverPorID(cboUsuarios.SelectedValue.ToString());
                this.chckAdministrar.Checked = _usuario.Administrar;
                this.chckCatalogos.Checked = _usuario.Catalogos;
                this.chckConsultas.Checked = _usuario.Consultar;
                this.chckDeshacerVenta.Checked = _usuario.DeshacerVenta;
                this.chckReportes.Checked = _usuario.Reportes;
                this.chckVender.Checked = _usuario.Vender;
                this.txtNombreReal.Text = _usuario.Nombre;
                this.txtApellido.Text = _usuario.Apellido;
                if (!_usuario.Administrar)
                    tabControl1.TabIndex = 2;
            }
            else
            {
                MessageBox.Show("Canceló la comprobación de usuario\n Se cerrará la ventana");
                this.Dispose();
            }
        }
        #region Pestaña Contraseñas
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Para ver si se puso correctamente las contraseñas en los textbox
            bool contraseñasOk = ComprobarContraseñas();

            if (contraseñasOk && _usuario.Contraseña == txtContraseñaActual.Text)
            {
                _usuario.Contraseña = txtNuevaContraseña2.Text;
                bool modificado = UsuarioBO.Modificar(_usuario);
                if (modificado)
                {
                    MessageBox.Show("Usuario Modificado");
                    //Si el usuario modificado es el usuario actual entonces le pongo la contraseña nueva al usuario actual
                    if (Sesion.UsuarioActual.UserName == _usuario.UserName)
                        Sesion.UsuarioActual.Contraseña = _usuario.Contraseña;
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Error en las contraseñas");
            }
        }


        bool ComprobarContraseñas()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtContraseñaActual.Text))
                ok = false;
            if (string.IsNullOrWhiteSpace(txtNuevaContraseña.Text))
                ok = false;
            if (string.IsNullOrWhiteSpace(txtNuevaContraseña2.Text))
                ok = false;
            if (!txtNuevaContraseña.Text.Equals(txtNuevaContraseña2.Text))
                ok = false;

            return ok;
        }

        #endregion

        #region Pestaña Privilegios
        private void btnOk_Click(object sender, EventArgs e)
        {
            frmConfirmarUsuario fconf = new frmConfirmarUsuario(cboUsuarios.SelectedItem.ToString());
            fconf.AlConfirmar += new frmConfirmarUsuario.AlConfirmarEventHandler(fconf_AlConfirmar);
            fconf.ShowDialog(this);
        }
        void PonerPrivilegios()
        {
            _usuario = UsuarioBO.DevolverPorID(cboUsuarios.SelectedItem.ToString());
            _usuario.Administrar = chckAdministrar.Checked;
            _usuario.Consultar = chckConsultas.Checked;
            _usuario.Reportes = chckReportes.Checked;
            _usuario.Vender = chckVender.Checked;
            _usuario.Catalogos = chckCatalogos.Checked;
            _usuario.DeshacerVenta = chckDeshacerVenta.Checked;
            UsuarioBO.Modificar(_usuario);

            //Si el usuario seleccionado para modificar es el mismo que el actual
            //entonces asignarle los cambios al actual
            if (_usuario.UserName == Sesion.UsuarioActual.UserName)
                Sesion.UsuarioActual = _usuario;
            MessageBox.Show("Hecho");
        }

        //Este método se ejecutará si el evento AlConfirmar del formulario de confirmarUsuario es ejecutado
        void fconf_AlConfirmar(bool valor)
        {
            if (valor)
                PonerPrivilegios();
            else
                MessageBox.Show("Usuario no confirmado\nCerrando...");
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion





        private void btnGo_Click(object sender, EventArgs e)
        {

        }



    }
}
