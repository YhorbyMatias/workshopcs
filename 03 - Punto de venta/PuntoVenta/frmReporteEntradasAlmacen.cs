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
    public partial class frmReporteEntradasAlmacen : Form
    {
        public frmReporteEntradasAlmacen()
        {
            InitializeComponent();
        }

        private void frmReporteEntradasAlmacen_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsProductos.EntradasAlmacen' table. You can move, or remove it, as needed.
            this.EntradasAlmacenTableAdapter.Fill(this.dsProductos.EntradasAlmacen);
            this.reportViewer1.RefreshReport();
            dtpFechaFinal.MinDate = dtpFechaInicial.Value;
            dtpFechaInicial.MaxDate = DateTime.Today;
            dtpFechaFinal.Enabled = false;
            radioUsuario.Checked = true;
            radioConFechas.Checked = true;
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                #region no usado
                //switch (opcion2)
                //{
                //    case OpcionFechas.BusquedaConFechas:
                //        if (chckFechaFinal.Checked)
                //            this.EntradasAlmacenTableAdapter.FillByUsuarioDosFechas(dsProductos.EntradasAlmacen, txtBusqueda.Text, dtpFechaInicial.Value.ToShortDateString(), dtpFechaFinal.Value.ToShortDateString());
                //        else
                //            this.EntradasAlmacenTableAdapter.FillByUsuarioYFecha(dsProductos.EntradasAlmacen, txtBusqueda.Text, dtpFechaFinal.Value.ToShortDateString());
                //        break;
                //    case OpcionFechas.BusquedaSinFechas:
                //        this.EntradasAlmacenTableAdapter.FillByNombreUsuario(dsProductos.EntradasAlmacen, txtBusqueda.Text);
                //        break;
                //    case OpcionFechas.BusquedaSoloFechas:
                //        if (chckFechaFinal.Checked)
                //            this.EntradasAlmacenTableAdapter.FillByDosFechas(dsProductos.EntradasAlmacen, dtpFechaInicial.Value.ToShortDateString(), dtpFechaFinal.Value.ToShortDateString());
                //        else
                //            this.EntradasAlmacenTableAdapter.FillByFecha(dsProductos.EntradasAlmacen, dtpFechaInicial.Value.ToShortDateString());
                //        break;
                //}
                //switch (opcion1)
                //{
                //    case OpcionBusqueda.BusquedaPorId:
                //        this.EntradasAlmacenTableAdapter.FillByIdProducto(dsProductos.EntradasAlmacen, txtBusqueda.Text);
                //        break;
                //    case OpcionBusqueda.BusquedaPorUsuario:
                //        break;
                //}
                #endregion

                ComprobarOpciones();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(this, excepcion.Message, "Excepción lanzada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TipoOpcion_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == radioIdProducto && radioIdProducto.Checked)
            {
                groupBoxFechas.Enabled = false;
                radioSinFechas.Checked = true;
            }
            else
            {
                groupBoxFechas.Enabled = true;
            }

            if (sender == radioUsuario)
            {
                if (!radioSinFechas.Checked)
                    groupBoxFechas.Enabled = true;
            }
            else if (sender == radioConFechas)
            {
                groupBoxBusqueda.Enabled = true;
                radioUsuario.Checked = true;
            }
            else if (sender == radioSinFechas && radioSinFechas.Checked)
            {
                groupBoxFechas.Enabled = false;
                dtpFechaFinal.Checked = false;
                dtpFechaInicial.Checked = false;
                groupBoxBusqueda.Enabled = true;
                chckFechaFinal.Checked = false;
                radioUsuario.Checked = true;
            }
            else if (sender == radioSoloFecha && radioSoloFecha.Checked)
            {
                groupBoxBusqueda.Enabled = false;
                groupBoxFechas.Enabled = true;
                radioIdProducto.Checked = false;
                radioUsuario.Checked = false;
            }
            else if (sender == chckFechaFinal && chckFechaFinal.Checked)
            {
                dtpFechaFinal.Enabled = true;
            }
            

        }


        void ComprobarOpciones()
        {
            StringBuilder sb=new StringBuilder();
            sb.Clear();
            sb.Append(txtBusqueda.Text);
            DateTime fechaInicial=dtpFechaInicial.Value;
            DateTime fechaFinal = dtpFechaFinal.Value;

            if (radioIdProducto.Checked)
                this.EntradasAlmacenTableAdapter.FillByIdProducto(dsProductos.EntradasAlmacen, sb.ToString());
            else if ((radioUsuario.Checked && radioConFechas.Checked) && chckFechaFinal.Checked)
                this.EntradasAlmacenTableAdapter.FillByUsuarioDosFechas(dsProductos.EntradasAlmacen, sb.ToString(), fechaInicial.ToShortDateString(), fechaFinal.ToShortDateString());
            else if ((radioUsuario.Checked && radioConFechas.Checked) && !chckFechaFinal.Checked)
                this.EntradasAlmacenTableAdapter.FillByUsuarioYFecha(dsProductos.EntradasAlmacen, sb.ToString(), fechaInicial.ToShortDateString());
            else if (radioUsuario.Checked && !radioConFechas.Checked)
                this.EntradasAlmacenTableAdapter.FillByNombreUsuario(dsProductos.EntradasAlmacen, sb.ToString());
            else if (radioSoloFecha.Checked && chckFechaFinal.Checked)
                this.EntradasAlmacenTableAdapter.FillByDosFechas(dsProductos.EntradasAlmacen, fechaInicial.ToShortDateString(), fechaFinal.ToShortDateString());
            else if (radioSoloFecha.Checked && !chckFechaFinal.Checked)
                this.EntradasAlmacenTableAdapter.FillByFecha(dsProductos.EntradasAlmacen, fechaInicial.ToShortDateString());

            this.reportViewer1.RefreshReport();

            sb=null;
        }

        private void frmReporteEntradasAlmacen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Esto es para evitar  el error al cerrar
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }


    }
}
