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
    public partial class Form2 : Form
    {
        usuariosCN objusuarioscn = new usuariosCN();
        public string var_nombre;
        public Form2()
        {
            InitializeComponent();
        }
        
        private void crearUusarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 crear_usuario = new Form3();
            crear_usuario.ShowDialog();
        }
        void conocerrol()
        {
            String rpta = "";
            try
            {
                ceUsuarios objusuariose = new ceUsuarios();

                objusuariose.Usuario = var_nombre;

                rpta = objusuarioscn.Conocer_Rol(objusuariose);
                toolStripStatusLabel4.Text = rpta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(rpta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = var_nombre;
            conocerrol();
        }

        private void modificarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 modificar_usuario = new Form4();
            modificar_usuario.ShowDialog();
        }
    }
}
