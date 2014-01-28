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
    public partial class frmEliminarUsuario : Form
    {
        public frmEliminarUsuario()
        {
            InitializeComponent();
        }
        List<Usuario> _listaUsuarios;
        private void frmEliminarUsuario_Load(object sender, EventArgs e)
        {
            _listaUsuarios = UsuarioBO.DevolverTodos();
            cboUsuarios.DataSource = _listaUsuarios;
            cboUsuarios.DisplayMember = "Usuario.UserName";
            cboUsuarios.ValueMember = "UserName";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cboUsuarios.SelectedItem.ToString().Equals(Sesion.UsuarioActual.UserName))
                MessageBox.Show("No te puedes eliminar a tí mismo");
            else
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar el usuario?", "Aviso", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    MessageBox.Show(Sesion.UsuarioActual.UserName);
                    bool ok = UsuarioBO.EliminarUsuario(cboUsuarios.SelectedItem.ToString(), Sesion.UsuarioActual.UserName, txtComentarios.Text);
                    if (ok)
                    {
                        MessageBox.Show("Eliminado");
                        this.Dispose();
                    }
                    else
                        MessageBox.Show("Error");
                }
            }
        }
    }
}
