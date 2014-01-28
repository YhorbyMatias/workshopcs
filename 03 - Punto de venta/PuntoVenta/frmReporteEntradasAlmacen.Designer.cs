namespace PuntoVenta
{
    partial class frmReporteEntradasAlmacen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.EntradasAlmacenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsProductos = new PuntoVenta.dsProductos();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.EntradasAlmacenTableAdapter = new PuntoVenta.dsProductosTableAdapters.EntradasAlmacenTableAdapter();
            this.groupBoxFechas = new System.Windows.Forms.GroupBox();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.chckFechaFinal = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioSinFechas = new System.Windows.Forms.RadioButton();
            this.radioSoloFecha = new System.Windows.Forms.RadioButton();
            this.radioConFechas = new System.Windows.Forms.RadioButton();
            this.groupBoxBusqueda = new System.Windows.Forms.GroupBox();
            this.radioUsuario = new System.Windows.Forms.RadioButton();
            this.radioIdProducto = new System.Windows.Forms.RadioButton();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.EntradasAlmacenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).BeginInit();
            this.groupBoxFechas.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxBusqueda.SuspendLayout();
            this.SuspendLayout();
            // 
            // EntradasAlmacenBindingSource
            // 
            this.EntradasAlmacenBindingSource.DataMember = "EntradasAlmacen";
            this.EntradasAlmacenBindingSource.DataSource = this.dsProductos;
            // 
            // dsProductos
            // 
            this.dsProductos.DataSetName = "dsProductos";
            this.dsProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "DatasetEntradaAlmacen";
            reportDataSource1.Value = this.EntradasAlmacenBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "PuntoVenta.Reportes.ReporteEntradasAlmacen.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 186);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(820, 395);
            this.reportViewer1.TabIndex = 19;
            // 
            // btnMostrar
            // 
            this.btnMostrar.Location = new System.Drawing.Point(618, 12);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(59, 149);
            this.btnMostrar.TabIndex = 18;
            this.btnMostrar.Text = "Mostrar";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // EntradasAlmacenTableAdapter
            // 
            this.EntradasAlmacenTableAdapter.ClearBeforeFill = true;
            // 
            // groupBoxFechas
            // 
            this.groupBoxFechas.Controls.Add(this.dtpFechaFinal);
            this.groupBoxFechas.Controls.Add(this.chckFechaFinal);
            this.groupBoxFechas.Controls.Add(this.label2);
            this.groupBoxFechas.Controls.Add(this.dtpFechaInicial);
            this.groupBoxFechas.Location = new System.Drawing.Point(21, 94);
            this.groupBoxFechas.Name = "groupBoxFechas";
            this.groupBoxFechas.Size = new System.Drawing.Size(591, 67);
            this.groupBoxFechas.TabIndex = 25;
            this.groupBoxFechas.TabStop = false;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Location = new System.Drawing.Point(298, 36);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(259, 20);
            this.dtpFechaFinal.TabIndex = 21;
            // 
            // chckFechaFinal
            // 
            this.chckFechaFinal.AutoSize = true;
            this.chckFechaFinal.Location = new System.Drawing.Point(298, 16);
            this.chckFechaFinal.Name = "chckFechaFinal";
            this.chckFechaFinal.Size = new System.Drawing.Size(81, 17);
            this.chckFechaFinal.TabIndex = 19;
            this.chckFechaFinal.Text = "Fecha Final";
            this.chckFechaFinal.UseVisualStyleBackColor = true;
            this.chckFechaFinal.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Fecha Inicial:";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Location = new System.Drawing.Point(17, 34);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(262, 20);
            this.dtpFechaInicial.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioSinFechas);
            this.panel1.Controls.Add(this.radioSoloFecha);
            this.panel1.Controls.Add(this.radioConFechas);
            this.panel1.Location = new System.Drawing.Point(21, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 29);
            this.panel1.TabIndex = 26;
            // 
            // radioSinFechas
            // 
            this.radioSinFechas.AutoSize = true;
            this.radioSinFechas.Location = new System.Drawing.Point(374, 9);
            this.radioSinFechas.Name = "radioSinFechas";
            this.radioSinFechas.Size = new System.Drawing.Size(109, 17);
            this.radioSinFechas.TabIndex = 2;
            this.radioSinFechas.TabStop = true;
            this.radioSinFechas.Text = "Buscar sin fechas";
            this.radioSinFechas.UseVisualStyleBackColor = true;
            this.radioSinFechas.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // radioSoloFecha
            // 
            this.radioSoloFecha.AutoSize = true;
            this.radioSoloFecha.Location = new System.Drawing.Point(212, 9);
            this.radioSoloFecha.Name = "radioSoloFecha";
            this.radioSoloFecha.Size = new System.Drawing.Size(110, 17);
            this.radioSoloFecha.TabIndex = 1;
            this.radioSoloFecha.TabStop = true;
            this.radioSoloFecha.Text = "Buscar sólo fecha";
            this.radioSoloFecha.UseVisualStyleBackColor = true;
            this.radioSoloFecha.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // radioConFechas
            // 
            this.radioConFechas.AutoSize = true;
            this.radioConFechas.Location = new System.Drawing.Point(26, 9);
            this.radioConFechas.Name = "radioConFechas";
            this.radioConFechas.Size = new System.Drawing.Size(114, 17);
            this.radioConFechas.TabIndex = 0;
            this.radioConFechas.TabStop = true;
            this.radioConFechas.Text = "Buscar con fechas";
            this.radioConFechas.UseVisualStyleBackColor = true;
            this.radioConFechas.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // groupBoxBusqueda
            // 
            this.groupBoxBusqueda.Controls.Add(this.radioUsuario);
            this.groupBoxBusqueda.Controls.Add(this.radioIdProducto);
            this.groupBoxBusqueda.Controls.Add(this.txtBusqueda);
            this.groupBoxBusqueda.Controls.Add(this.label1);
            this.groupBoxBusqueda.Location = new System.Drawing.Point(21, 1);
            this.groupBoxBusqueda.Name = "groupBoxBusqueda";
            this.groupBoxBusqueda.Size = new System.Drawing.Size(591, 52);
            this.groupBoxBusqueda.TabIndex = 27;
            this.groupBoxBusqueda.TabStop = false;
            // 
            // radioUsuario
            // 
            this.radioUsuario.AutoSize = true;
            this.radioUsuario.Location = new System.Drawing.Point(453, 26);
            this.radioUsuario.Name = "radioUsuario";
            this.radioUsuario.Size = new System.Drawing.Size(61, 17);
            this.radioUsuario.TabIndex = 28;
            this.radioUsuario.TabStop = true;
            this.radioUsuario.Text = "Usuario";
            this.radioUsuario.UseVisualStyleBackColor = true;
            this.radioUsuario.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // radioIdProducto
            // 
            this.radioIdProducto.AutoSize = true;
            this.radioIdProducto.Location = new System.Drawing.Point(314, 26);
            this.radioIdProducto.Name = "radioIdProducto";
            this.radioIdProducto.Size = new System.Drawing.Size(80, 17);
            this.radioIdProducto.TabIndex = 27;
            this.radioIdProducto.TabStop = true;
            this.radioIdProducto.Text = "Id Producto";
            this.radioIdProducto.UseVisualStyleBackColor = true;
            this.radioIdProducto.CheckedChanged += new System.EventHandler(this.TipoOpcion_CheckedChanged);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(5, 24);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(289, 20);
            this.txtBusqueda.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Búsqueda:";
            // 
            // frmReporteEntradasAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 581);
            this.Controls.Add(this.groupBoxBusqueda);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxFechas);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnMostrar);
            this.Name = "frmReporteEntradasAlmacen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReporteEntradasAlmacen_FormClosing);
            this.Load += new System.EventHandler(this.frmReporteEntradasAlmacen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.EntradasAlmacenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).EndInit();
            this.groupBoxFechas.ResumeLayout(false);
            this.groupBoxFechas.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxBusqueda.ResumeLayout(false);
            this.groupBoxBusqueda.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.BindingSource EntradasAlmacenBindingSource;
        private dsProductos dsProductos;
        private dsProductosTableAdapters.EntradasAlmacenTableAdapter EntradasAlmacenTableAdapter;
        private System.Windows.Forms.GroupBox groupBoxFechas;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.CheckBox chckFechaFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioSoloFecha;
        private System.Windows.Forms.RadioButton radioConFechas;
        private System.Windows.Forms.RadioButton radioSinFechas;
        private System.Windows.Forms.GroupBox groupBoxBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioUsuario;
        private System.Windows.Forms.RadioButton radioIdProducto;
    }
}