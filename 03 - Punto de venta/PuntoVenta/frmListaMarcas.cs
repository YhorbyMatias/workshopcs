using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuntoVenta.Entidades;
using PuntoVenta.CapaNegocios;

namespace PuntoVenta
{
    public partial class frmListaMarcas : Form
    {
        public frmListaMarcas()
        {
            InitializeComponent();
        }
        public frmListaMarcas(bool modoEliminar)
        {
            InitializeComponent();
            _modoEliminar = true;
            label2.Text = "Seleccione la marca a eliminar";
            btnEditar.Image = null;
            btnEditar.Text = "Eliminar";
        }
        bool _modoEliminar;

        public List<Marca> ListaMarcas { get; set; }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmListaMarcas_Load(object sender, EventArgs e)
        {
            CargarMarcas();
            cboMarcas.DataSource = ListaMarcas;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_modoEliminar)
                {
                    DialogResult resultado = MessageBox.Show(this, "Está seguro de que desea eliminar la marca seleccionada?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (resultado == DialogResult.Yes)
                    {
                        MarcaBO.Eliminar(cboMarcas.SelectedValue.ToString());
                        MessageBox.Show("Eliminado");
                        this.Dispose();
                    }
                }
                else
                {
                    Marca marcaAModificar = new Marca
                    {
                        Id = Convert.ToInt32(cboMarcas.SelectedValue.ToString()),
                        Descripcion = cboMarcas.SelectedItem.ToString()
                    };
                    frmNuevaMarca fmodfmarca = new frmNuevaMarca(marcaAModificar);
                    fmodfmarca.AlModificar += new frmNuevaMarca.ModificarEventHandler(CargarMarcas);
                    fmodfmarca.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void CargarMarcas()
        {
            cboMarcas.DisplayMember = null;
            cboMarcas.ValueMember = null;
            cboMarcas.DataSource = null;
            ListaMarcas = MarcaBO.DevolverTodos();
            cboMarcas.DataSource = ListaMarcas;
            cboMarcas.DisplayMember = "Descripcion";
            cboMarcas.ValueMember = "Id";
        }

    }
}
