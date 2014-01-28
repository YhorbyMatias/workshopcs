namespace PuntoVenta
{
    partial class frmConsultaGanancia
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
            this.radioDias = new System.Windows.Forms.RadioButton();
            this.radioMeses = new System.Windows.Forms.RadioButton();
            this.gridGanancias = new System.Windows.Forms.DataGridView();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gananciaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gananciasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsProductosBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dsProductos = new PuntoVenta.dsProductos();
            this.dsProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSalir = new System.Windows.Forms.Button();
            this.gananciasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gananciasTableAdapter = new PuntoVenta.dsProductosTableAdapters.GananciasTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridGanancias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gananciasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductosBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gananciasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // radioDias
            // 
            this.radioDias.AutoSize = true;
            this.radioDias.Location = new System.Drawing.Point(25, 52);
            this.radioDias.Name = "radioDias";
            this.radioDias.Size = new System.Drawing.Size(65, 17);
            this.radioDias.TabIndex = 0;
            this.radioDias.TabStop = true;
            this.radioDias.Text = "Por días";
            this.radioDias.UseVisualStyleBackColor = true;
            this.radioDias.CheckedChanged += new System.EventHandler(this.radioDias_CheckedChanged);
            // 
            // radioMeses
            // 
            this.radioMeses.AutoSize = true;
            this.radioMeses.Location = new System.Drawing.Point(184, 52);
            this.radioMeses.Name = "radioMeses";
            this.radioMeses.Size = new System.Drawing.Size(74, 17);
            this.radioMeses.TabIndex = 0;
            this.radioMeses.TabStop = true;
            this.radioMeses.Text = "Por meses";
            this.radioMeses.UseVisualStyleBackColor = true;
            this.radioMeses.CheckedChanged += new System.EventHandler(this.radioMeses_CheckedChanged);
            // 
            // gridGanancias
            // 
            this.gridGanancias.AllowUserToAddRows = false;
            this.gridGanancias.AllowUserToDeleteRows = false;
            this.gridGanancias.AutoGenerateColumns = false;
            this.gridGanancias.ColumnHeadersHeight = 40;
            this.gridGanancias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridGanancias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaDataGridViewTextBoxColumn,
            this.gananciaDataGridViewTextBoxColumn});
            this.gridGanancias.DataSource = this.gananciasBindingSource1;
            this.gridGanancias.Location = new System.Drawing.Point(12, 92);
            this.gridGanancias.Name = "gridGanancias";
            this.gridGanancias.ReadOnly = true;
            this.gridGanancias.Size = new System.Drawing.Size(352, 358);
            this.gridGanancias.TabIndex = 1;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gananciaDataGridViewTextBoxColumn
            // 
            this.gananciaDataGridViewTextBoxColumn.DataPropertyName = "Ganancia";
            this.gananciaDataGridViewTextBoxColumn.HeaderText = "Ganancia";
            this.gananciaDataGridViewTextBoxColumn.Name = "gananciaDataGridViewTextBoxColumn";
            this.gananciaDataGridViewTextBoxColumn.ReadOnly = true;
            this.gananciaDataGridViewTextBoxColumn.Width = 200;
            // 
            // gananciasBindingSource1
            // 
            this.gananciasBindingSource1.DataMember = "Ganancias";
            this.gananciasBindingSource1.DataSource = this.dsProductosBindingSource1;
            // 
            // dsProductosBindingSource1
            // 
            this.dsProductosBindingSource1.DataSource = this.dsProductos;
            this.dsProductosBindingSource1.Position = 0;
            // 
            // dsProductos
            // 
            this.dsProductos.DataSetName = "dsProductos";
            this.dsProductos.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsProductosBindingSource
            // 
            this.dsProductosBindingSource.DataSource = this.dsProductos;
            this.dsProductosBindingSource.Position = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(302, 36);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(62, 33);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // gananciasBindingSource
            // 
            this.gananciasBindingSource.DataMember = "Ganancias";
            this.gananciasBindingSource.DataSource = this.dsProductosBindingSource;
            // 
            // gananciasTableAdapter
            // 
            this.gananciasTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha exacta:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(127, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(85, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 19);
            this.button1.TabIndex = 5;
            this.button1.Text = "Ver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmConsultaGanancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 462);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.gridGanancias);
            this.Controls.Add(this.radioMeses);
            this.Controls.Add(this.radioDias);
            this.Name = "frmConsultaGanancia";
            this.Load += new System.EventHandler(this.frmConsultaGanancia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridGanancias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gananciasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductosBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsProductosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gananciasBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioDias;
        private System.Windows.Forms.RadioButton radioMeses;
        private System.Windows.Forms.DataGridView gridGanancias;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.BindingSource dsProductosBindingSource;
        private dsProductos dsProductos;
        private System.Windows.Forms.BindingSource dsProductosBindingSource1;
        private System.Windows.Forms.BindingSource gananciasBindingSource;
        private dsProductosTableAdapters.GananciasTableAdapter gananciasTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gananciaDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource gananciasBindingSource1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
    }
}