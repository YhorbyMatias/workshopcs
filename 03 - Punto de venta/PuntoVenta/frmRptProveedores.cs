using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class frmRptProveedores : Form
    {
        public frmRptProveedores()
        {
            InitializeComponent();
        }

        private void frmRptProveedores_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsProductos.Proveedores' table. You can move, or remove it, as needed.
            this.ProveedoresTableAdapter.Fill(this.dsProductos.Proveedores);

            this.reportViewer1.RefreshReport();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                ComprobarBusqueda();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(this, excepcion.Message, "Excepción lanzada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ComprobarBusqueda()
        {
            if (radioId.Checked)
            {
                this.ProveedoresTableAdapter.FillById(this.dsProductos.Proveedores, txtBuscar.Text);
            }
            else
                if (radioNombre.Checked)
                {
                    this.ProveedoresTableAdapter.FillByNombre(this.dsProductos.Proveedores, txtBuscar.Text);
                }
                else
                {
                    this.ProveedoresTableAdapter.Fill(this.dsProductos.Proveedores);
                }
            this.reportViewer1.RefreshReport();
        }
    }
}
