using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.CapaNegocios;
using PuntoVenta.Entidades;

namespace PuntoVenta
{
    public partial class frmNuevaCategoria : Form
    {
        public frmNuevaCategoria()
        {
            InitializeComponent();
        }
        //EVENTO PARA HACER SABER QUE TODO FUE OK
        public delegate void HechoEventHandler(bool ok);
        public event HechoEventHandler Hecho;

        public Categoria CategoriaCreada { get; set; }

        private void frmNuevaEditCategoria_Load(object sender, EventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Comprobar())
            {
                Categoria _categoria = new Categoria();
                _categoria.Descripcion = txtDescripcion.Text;

                //Comprobando si existe la categoría
                if (CategoriaBO.Existe(_categoria))
                {
                    DialogResult resultado = MessageBox.Show("La descripción de la categoría concuerda \n con una que existe, escriba otra");
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro?", "Guardar categoría", MessageBoxButtons.YesNo);
                    if (resultado == DialogResult.No)
                        txtDescripcion.Focus();
                    else
                    {
                        _categoria.IdCategoria = CategoriaBO.Crear(_categoria);

                        CategoriaCreada = _categoria;

                        MessageBox.Show(string.Format("Categoría creada: {0} Id: ({1})",
                                                        _categoria.Descripcion, _categoria.IdCategoria));

                        if (Hecho != null)
                            Hecho(true);

                        this.DialogResult = DialogResult.OK;

                        this.Close();
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Hecho != null)
                Hecho(false);

            this.DialogResult = DialogResult.Cancel;

            this.Dispose();
        }

        bool Comprobar()
        {
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("No deje a descripción en blanco");
                ok = false;
            }

            return ok;
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }




    }
}
