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
    public partial class frmSeleccionarProveedor : Form
    {
        public frmSeleccionarProveedor()
        {
            InitializeComponent();
        }
        List<Proveedor> _listaProveedores;

        private void frmSeleccionarProveedor_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        void CargarProveedores()
        {
            /*
             * Cargando proveedores
             * 
             * */
            _listaProveedores = ProveedorBO.DevolverTodos();
            cboProveedores.DataSource = _listaProveedores;
            cboProveedores.ValueMember = "Nombre";
            cboProveedores.DisplayMember = "Nombre";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Proveedor proAModif = new Proveedor();
            proAModif = (from x in _listaProveedores
                         where x.IdProveedor.ToString() == cboProveedores.SelectedValue.ToString()
                         select x).SingleOrDefault();

            frmNuevoProveedor fmodpro = new frmNuevoProveedor(proAModif);
            fmodpro.ShowDialog();
        }
    }
}
