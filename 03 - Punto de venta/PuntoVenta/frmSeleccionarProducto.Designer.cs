﻿namespace PuntoVenta
{
    partial class frmSeleccionarProducto
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboProducto = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblUnidadMedida = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto:";
            // 
            // comboProducto
            // 
            this.comboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboProducto.FormattingEnabled = true;
            this.comboProducto.Location = new System.Drawing.Point(172, 30);
            this.comboProducto.Name = "comboProducto";
            this.comboProducto.Size = new System.Drawing.Size(259, 39);
            this.comboProducto.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidad.Location = new System.Drawing.Point(210, 137);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(97, 35);
            this.nudCantidad.TabIndex = 3;
            // 
            // lblUnidadMedida
            // 
            this.lblUnidadMedida.AutoSize = true;
            this.lblUnidadMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadMedida.Location = new System.Drawing.Point(316, 140);
            this.lblUnidadMedida.Name = "lblUnidadMedida";
            this.lblUnidadMedida.Size = new System.Drawing.Size(116, 29);
            this.lblUnidadMedida.TabIndex = 2;
            this.lblUnidadMedida.Text = "Unidades";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 57);
            this.panel1.TabIndex = 5;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Crimson;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Location = new System.Drawing.Point(230, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(212, 52);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Location = new System.Drawing.Point(0, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(212, 52);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Proceder";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(443, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Seleccione producto y cantidad";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "Precio: ";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPrecio.Location = new System.Drawing.Point(166, 88);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(0, 31);
            this.lblPrecio.TabIndex = 0;
            // 
            // frmSeleccionarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 256);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.lblUnidadMedida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboProducto);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSeleccionarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSeleccionarProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboProducto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblUnidadMedida;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPrecio;
    }
}