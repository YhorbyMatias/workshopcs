using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Form3 : Form
    {
        usuariosCN objusuarioscn = new usuariosCN();
        public Form3()
        {
            InitializeComponent();
        }
        #region metodos generales

        String validacion()
        {
            if (txtUsuario.Text.Equals(""))
            {
                return "Ingrese Usuario del Usuario";
            }
            else if (txtClave.Text.Equals(""))
            {
                return "Ingrese Clave del Usuario";
            }
            else if (txtEstado.Text.Equals("Seleccione un Estado"))
            {
                return "Ingrese Estado del Usuario";
            }
            else if (txtRol.Text.Equals("Seleccione un Rol"))
            {
                return "Ingrese Rol del Usuario";
            }
            else
            {
                return "ok";
            }
        }

        void Limpiar()
        {
            txtUsuario.Text = "";
            txtClave.Text = "";
            txtEstado.Text = "Seleccione un Estado";
            txtRol.Text = "Seleccione un Rol";
        }

        #endregion

        void GuardarUsuarios()
        {
            String rpta = "";
            try
            {
                ceUsuarios objusuariose = new ceUsuarios();
                if (validacion() == "ok")
                {
                    objusuariose.Usuario = txtUsuario.Text;
                    objusuariose.Clave = txtClave.Text;
                    objusuariose.Estado = txtEstado.Text;
                    objusuariose.Rol = txtRol.Text;

                    rpta = objusuarioscn.RegistrarUsuarios(objusuariose);
                    MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {  
                    MessageBox.Show(validacion(), "Messaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ActualizarUsuarios()
        {
            String rpta = "";
            try
            {
                ceUsuarios objusuariose = new ceUsuarios();
                if (validacion() == "ok")
                {
                    objusuariose.Usuario = txtUsuario.Text;
                    objusuariose.Clave = txtClave.Text;
                    objusuariose.Estado = txtEstado.Text;
                    objusuariose.Rol = txtRol.Text;

                    rpta = objusuarioscn.ModificarUsuarios(objusuariose);
                    MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(validacion(), "Messaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void generaCodigo()
        {
            txtid.Text = Convert.ToString(objusuarioscn.Ultimocodigousuarios());
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            generaCodigo();
            txtid.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarUsuarios();
        }
    }
}
