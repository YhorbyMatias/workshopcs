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
    public partial class Form4 : Form
    {
        usuariosCN objusuarioscn = new usuariosCN();
        public Form4()
        {
            InitializeComponent();
        }
        #region metodos generales

        String validacion()
        {
            if (comboBox1.Text.Equals(""))
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
            comboBox1.Text = "";
            txtClave.Text = "";
            txtEstado.Text = "Seleccione un Estado";
            txtRol.Text = "Seleccione un Rol";
        }

        #endregion

        void ActualizarUsuarios()
        {
            String rpta = "";
            try
            {
                ceUsuarios objusuariose = new ceUsuarios();
                if (validacion() == "ok")
                {
                    objusuariose.Usuario = comboBox1.Text;
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

        void cargaUsuarios()
        {
            comboBox1.DataSource = objusuarioscn.ListaUsuarios().Tables["usuarios"];
            comboBox1.DisplayMember = "usuario";
            comboBox1.ValueMember = "ID";
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            cargaUsuarios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActualizarUsuarios();
        }
    }
}
