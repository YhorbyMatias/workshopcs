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
    public partial class frmConfirmarUsuario : Form
    {
        //public frmConfirmarUsuario()
        //{
        //    InitializeComponent();
        //}

        public frmConfirmarUsuario(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }
        string userName;
        public delegate void AlConfirmarEventHandler(bool valor);
        public event AlConfirmarEventHandler AlConfirmar;

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (UsuarioBO.Comprobar(userName, txtContraseña.Text))
            {
                if (AlConfirmar != null)
                    AlConfirmar(true);
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
            }

        }

        private void frmConfirmarUsuario_Load(object sender, EventArgs e)
        {
            lblUserName.Text = userName;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (AlConfirmar != null)
                AlConfirmar(false);
            this.Close();
        }
    }
}
