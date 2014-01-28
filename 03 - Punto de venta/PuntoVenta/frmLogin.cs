using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.CapaNegocios;

namespace PuntoVenta
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        #region Propiedades
        //campo para guardar los intentos
        private int _intentos = 0;

        //Propiedad para controlar los intentos
        private int Intentos
        {
            get { return _intentos; }
            set
            {
                //para evitar que se ponga número negativo
                _intentos = (value < 0) ? 0 : value;
                //Encontré que la mejor manera de controlar que a las 3 veces
                //de poner datos incorrectos se cierre, es aquí en el set
                if (_intentos == 3)
                {
                    MessageBox.Show("Ha insertado claves erroneas 3 veces\nEl programa se cerrará");
                    this.Close();
                }
            }
        }
        #endregion


        private void frmLogin_Load(object sender, EventArgs e)
        {
            pictureBox1.Parent = this;

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            bool listo = ComprobarControles();
            if (listo)
            {
                error.Clear();
                bool usuarioCorrecto = false;
                try
                {
                    usuarioCorrecto = UsuarioBO.Comprobar(txtUsuario.Text, txtContraseña.Text);

                    if (usuarioCorrecto)
                    {
                        //Como el usuario fue correctamente identificado, pongo como usuario actual
                        //el usuario validado
                        Sesion.UsuarioActual = UsuarioBO.DevolverPorID(txtUsuario.Text);
                        frmPrincipal frprincipal = new frmPrincipal();
                        this.Hide();
                        //Para cuando se cierre cerrar este.
                        frprincipal.AlCerrarse += new frmPrincipal.AlCerrarseEventHandler(frprincipal_AlCerrarse);
                        frprincipal.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o contraseña incorrectos");
                        //Por cada intento erróneo de loguearse aumentar en 1 la variable _intentos
                        Intentos++;
                        label3.Text = Intentos.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region Métodos personalizados
        void frprincipal_AlCerrarse(bool valor)
        {
            if (valor)
                this.Close();
            else
            {
                this.Show();
                txtUsuario.Text = "";
                txtContraseña.Text = "";
                Intentos = 0;
                label3.Text = Intentos.ToString();
            }
        }



        bool ComprobarControles()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                error.SetError(txtUsuario, "No deje el nombre de usuario vacío");
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                error.SetError(txtContraseña, "No deje el campo de contraseña vacío");
                ok = false;
            }
            return ok;
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }


    }
}

