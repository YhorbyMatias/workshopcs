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
    public partial class frmBuscarFactura : Form
    {
        public frmBuscarFactura()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var x = FacturaBO.BuscarPorFolio(Convert.ToInt32(txtBusqueda.Text));
            if (x == null)
                MessageBox.Show("No existe una factura con ese número");
            else
            {
                frmModificarFactura fmodfact = new frmModificarFactura(x.Folio);
                fmodfact.ShowDialog();
            }
        }

    }
}
