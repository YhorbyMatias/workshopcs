using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.dsProductosTableAdapters;
using Microsoft.Reporting.WinForms;

namespace PuntoVenta
{
    public partial class frmRptProductosPorFecha : Form
    {
        public frmRptProductosPorFecha()
        {
            InitializeComponent();
        }

        void MostrarReporte()
        {
            if (chckFechaFinal.Checked)
            {
                EntradasProductosTableAdapter.FillByFechaBetween(dsProductos.EntradasProductos,dtpFechaInicial.Value.ToShortDateString(), dtpFechaFinal.Value.ToShortDateString());
                reportViewer1.RefreshReport();
            }
            else
            {
                EntradasProductosTableAdapter.FillByFecha(dsProductos.EntradasProductos, dtpFechaInicial.Value.ToShortDateString());
                reportViewer1.RefreshReport();
            }

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EntradasProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsProductos = new PuntoVenta.dsProductos();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.chckFechaFinal = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EntradasProductosTableAdapter = new PuntoVenta.dsProductosTableAdapters.EntradasProductosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.EntradasProductosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // EntradasProductosBindingSource
            // 
            this.EntradasProductosBindingSource.DataMember = "EntradasProductos";
            this.EntradasProductosBindingSource.DataSource = this.dsProductos;
            // 
            // dsProductos
            // 
            this.dsProductos.DataSetName = "dsProductos";
            this.dsProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(441, 44);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(54, 23);
            this.btnMostrar.TabIndex = 12;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(226, 44);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaFinal.TabIndex = 11;
            // 
            // chckFechaFinal
            // 
            this.chckFechaFinal.AutoSize = true;
            this.chckFechaFinal.Location = new System.Drawing.Point(226, 26);
            this.chckFechaFinal.Name = "chckFechaFinal";
            this.chckFechaFinal.Size = new System.Drawing.Size(81, 17);
            this.chckFechaFinal.TabIndex = 9;
            this.chckFechaFinal.Text = "Fecha Final";
            this.chckFechaFinal.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Fecha Inicial:";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Location = new System.Drawing.Point(6, 44);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(202, 20);
            this.dtpFechaInicial.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(744, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Movimiento de creacion o entrada de nuevos productos por fecha";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.EntradasProductosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PuntoVenta.Reportes.ReportMovimientoProductos.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 93);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(744, 321);
            this.reportViewer1.TabIndex = 13;
            // 
            // EntradasProductosTableAdapter
            // 
            this.EntradasProductosTableAdapter.ClearBeforeFill = true;
            // 
            // frmRptProductosPorFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 414);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.chckFechaFinal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFechaInicial);
            this.Controls.Add(this.label1);
            this.Name = "frmRptProductosPorFecha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRptProductosPorFecha_FormClosing);
            this.Load += new System.EventHandler(this.frmRptProductosPorFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntradasProductosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void frmRptProductosPorFecha_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsProductos.EntradasProductos' table. You can move, or remove it, as needed.
            //this.EntradasProductosTableAdapter.Fill(this.dsProductos.EntradasProductos);

            //this.reportViewer1.RefreshReport();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            MostrarReporte();
        }

        private void frmRptProductosPorFecha_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }


    }
}
