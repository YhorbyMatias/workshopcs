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
    public partial class frmCambio : Form
    {
        public frmCambio()
        {
            InitializeComponent();

            AcceptButton = btnAceptar;

            CancelButton = btnCancelar;
        }

        decimal _total;
        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;

                lblTotal.Text = string.Format("{0:C}", Total);
            }
        }

        private void frmCambio_Load(object sender, EventArgs e)
        {

            nudRecibido.Minimum = Total;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Dispose();
        }

        private void nudRecibido_ValueChanged(object sender, EventArgs e)
        {
            lblCambio.Text = string.Format("{0:C}",(nudRecibido.Value-Total));
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Dispose();
        }

   

    }
}
