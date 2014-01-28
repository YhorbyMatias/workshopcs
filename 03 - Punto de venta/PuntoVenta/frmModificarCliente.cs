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
    public partial class frmModificarCliente : Form
    {
        public frmModificarCliente()
        {
            InitializeComponent();
        }

        public frmModificarCliente(bool modoBorrar)
        {
            InitializeComponent();
            if (modoBorrar)
            {
                _modoBorrado = true;
                btnEditar.Image = null;
                btnEditar.Text = "Eliminar";
            }
        }
        List<Cliente> _listaClientes;
        bool _modoBorrado = false;
        private void frmModificarCliente_Load(object sender, EventArgs e)
        {
            _listaClientes = ClienteBO.DevolverTodos();
            cboClientes.DataSource = _listaClientes;
            cboClientes.DisplayMember = "Nombre";
        }


        void fnc_AlCerrar()
        {
            this.Dispose(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_modoBorrado)
            {
                DialogResult resultado = MessageBox.Show("¿Estás seguro?", "Aviso", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    Cliente cliente = (Cliente)cboClientes.SelectedItem;
                    if (ClienteBO.Eliminar(cliente.Id))
                    {
                        MessageBox.Show("Eliminado");
                        this.Dispose();
                    }
                    else
                        MessageBox.Show("Error al eliminar");
                }
            }
            else
            {
                Cliente c = (Cliente)cboClientes.SelectedItem;
                frmNuevoCliente fnc = new frmNuevoCliente(c);
                fnc.ShowDialog(this);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }


    }
}
