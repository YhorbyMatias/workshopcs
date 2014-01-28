namespace PuntoVenta
{
    partial class frmCrearUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrearUsuario));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRepetirContraseña = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtContraseña = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAdministrar = new System.Windows.Forms.CheckBox();
            this.chkVender = new System.Windows.Forms.CheckBox();
            this.chkCatalogos = new System.Windows.Forms.CheckBox();
            this.chkReportes = new System.Windows.Forms.CheckBox();
            this.chkConsultas = new System.Windows.Forms.CheckBox();
            this.chkDeshacerVenta = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.error = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtApellido);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNombre);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(266, 71);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(129, 40);
            this.txtApellido.MaxLength = 50;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(116, 20);
            this.txtApellido.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(129, 14);
            this.txtNombre.MaxLength = 50;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(116, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nombre real:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRepetirContraseña);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtContraseña);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 111);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // txtRepetirContraseña
            // 
            this.txtRepetirContraseña.Location = new System.Drawing.Point(129, 78);
            this.txtRepetirContraseña.MaxLength = 50;
            this.txtRepetirContraseña.Name = "txtRepetirContraseña";
            this.txtRepetirContraseña.Size = new System.Drawing.Size(116, 20);
            this.txtRepetirContraseña.TabIndex = 5;
            this.txtRepetirContraseña.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Repetir contraseña:";
            // 
            // txtContraseña
            // 
            this.txtContraseña.Location = new System.Drawing.Point(129, 45);
            this.txtContraseña.MaxLength = 50;
            this.txtContraseña.Name = "txtContraseña";
            this.txtContraseña.Size = new System.Drawing.Size(116, 20);
            this.txtContraseña.TabIndex = 3;
            this.txtContraseña.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(129, 13);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(116, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de usuario:";
            // 
            // chkAdministrar
            // 
            this.chkAdministrar.AutoSize = true;
            this.chkAdministrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkAdministrar.Location = new System.Drawing.Point(10, 196);
            this.chkAdministrar.Name = "chkAdministrar";
            this.chkAdministrar.Size = new System.Drawing.Size(75, 17);
            this.chkAdministrar.TabIndex = 6;
            this.chkAdministrar.Text = "Administrar";
            this.chkAdministrar.UseVisualStyleBackColor = true;
            // 
            // chkVender
            // 
            this.chkVender.AutoSize = true;
            this.chkVender.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkVender.Location = new System.Drawing.Point(176, 196);
            this.chkVender.Name = "chkVender";
            this.chkVender.Size = new System.Drawing.Size(58, 17);
            this.chkVender.TabIndex = 6;
            this.chkVender.Text = "Vender";
            this.chkVender.UseVisualStyleBackColor = true;
            // 
            // chkCatalogos
            // 
            this.chkCatalogos.AutoSize = true;
            this.chkCatalogos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkCatalogos.Location = new System.Drawing.Point(94, 196);
            this.chkCatalogos.Name = "chkCatalogos";
            this.chkCatalogos.Size = new System.Drawing.Size(71, 17);
            this.chkCatalogos.TabIndex = 6;
            this.chkCatalogos.Text = "Catalogos";
            this.chkCatalogos.UseVisualStyleBackColor = true;
            // 
            // chkReportes
            // 
            this.chkReportes.AutoSize = true;
            this.chkReportes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkReportes.Location = new System.Drawing.Point(10, 219);
            this.chkReportes.Name = "chkReportes";
            this.chkReportes.Size = new System.Drawing.Size(67, 17);
            this.chkReportes.TabIndex = 6;
            this.chkReportes.Text = "Reportes";
            this.chkReportes.UseVisualStyleBackColor = true;
            // 
            // chkConsultas
            // 
            this.chkConsultas.AutoSize = true;
            this.chkConsultas.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkConsultas.Location = new System.Drawing.Point(94, 219);
            this.chkConsultas.Name = "chkConsultas";
            this.chkConsultas.Size = new System.Drawing.Size(70, 17);
            this.chkConsultas.TabIndex = 6;
            this.chkConsultas.Text = "Consultas";
            this.chkConsultas.UseVisualStyleBackColor = true;
            // 
            // chkDeshacerVenta
            // 
            this.chkDeshacerVenta.AutoSize = true;
            this.chkDeshacerVenta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chkDeshacerVenta.Location = new System.Drawing.Point(176, 219);
            this.chkDeshacerVenta.Name = "chkDeshacerVenta";
            this.chkDeshacerVenta.Size = new System.Drawing.Size(100, 17);
            this.chkDeshacerVenta.TabIndex = 6;
            this.chkDeshacerVenta.Text = "Deshacer venta";
            this.chkDeshacerVenta.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(284, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 240);
            this.panel1.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Image = global::PuntoVenta.Properties.Resources.cancel;
            this.btnCancelar.Location = new System.Drawing.Point(0, 196);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 44);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Image = global::PuntoVenta.Properties.Resources.check;
            this.btnAceptar.Location = new System.Drawing.Point(0, 146);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(95, 44);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // error
            // 
            this.error.ContainerControl = this;
            // 
            // frmCrearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 240);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkConsultas);
            this.Controls.Add(this.chkCatalogos);
            this.Controls.Add(this.chkDeshacerVenta);
            this.Controls.Add(this.chkVender);
            this.Controls.Add(this.chkReportes);
            this.Controls.Add(this.chkAdministrar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCrearUsuario";
            this.Text = "Crear un usuario";
            this.Load += new System.EventHandler(this.frmCrearUsuario_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRepetirContraseña;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContraseña;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkAdministrar;
        private System.Windows.Forms.CheckBox chkVender;
        private System.Windows.Forms.CheckBox chkCatalogos;
        private System.Windows.Forms.CheckBox chkReportes;
        private System.Windows.Forms.CheckBox chkConsultas;
        private System.Windows.Forms.CheckBox chkDeshacerVenta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ErrorProvider error;





    }
}