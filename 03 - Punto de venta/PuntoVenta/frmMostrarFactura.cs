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
    public partial class frmMostrarFactura : Form
    {
        public frmMostrarFactura()
        {
            InitializeComponent();
        }

        public frmMostrarFactura(int folio):this()
        {
            Folio = folio;
        }

        public int Folio { get; set; }
        private void frmMostrarFactura_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsProductos.Factura' table. You can move, or remove it, as needed.
            this.FacturaTableAdapter.FillByFolio(this.dsProductos.Factura,Folio);

            this.reportViewer1.RefreshReport();
        }

        private void frmMostrarFactura_FormClosing(object sender, FormClosingEventArgs e)
        {

            //Esto es para evitar  el error al cerrar
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
