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
    public partial class Form1 : Form
    {
        usuariosCN objusuarioscn = new usuariosCN();
        public Form1()
        {
            InitializeComponent();
        }
        #region metodos generales

        String validacion()
        {
            if (txtClave.Text.Equals(""))
            {
                return "Ingrese Usuario y Clave de su Usuario";
            }
            else
            {
                return "ok";
            }
        }

        void Limpiar()
        {
            txtClave.Text = "";
        }

        #endregion
        void cargaUsuarios()
        {
            comboBox1.DataSource = objusuarioscn.ListaUsuarios().Tables["usuarios"];
            comboBox1.DisplayMember = "usuario";
            comboBox1.ValueMember = "ID";
        }
        
        void logearse()
        {
            String rpta = "";
            try
            {
                ceUsuarios objusuariose = new ceUsuarios();
                if (validacion() == "ok")
                {
                    objusuariose.Usuario = comboBox1.Text;
                    objusuariose.Clave = txtClave.Text;


                    rpta = objusuarioscn.logear(objusuariose);
                    if (rpta != "ingresar datos correctos")
                    {
                        MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      
                    }
                    else 
                    {
                        MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                       
                        return;
                    }
                    this.Hide();
                    Form2 frmDos = new Form2();
                    string selecionado = Convert.ToString(comboBox1.Text);
                    frmDos.var_nombre = selecionado;
                    frmDos.Show();
                }
                else
                {
                    MessageBox.Show(validacion(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(rpta, "Messaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cargaUsuarios();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            logearse();
        }
    }
}
