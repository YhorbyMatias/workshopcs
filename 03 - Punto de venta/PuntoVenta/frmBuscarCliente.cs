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
    /// <summary>
    /// Para controlar las opciones de búsqueda
    /// </summary>
    enum OpcionBusqueda
    {
        Id,
        Nombre,
        Apellido,
        Cedula,
        Indeterminado
    }

    public partial class frmBuscarCliente : Form
    {
        public frmBuscarCliente()
        {
            InitializeComponent();
            _listaClientes = new List<Cliente>();
        }
        OpcionBusqueda opcion = OpcionBusqueda.Indeterminado;

        #region Propiedades

        //Propiedad que guarda el Id del cliente que se eligió
        private string _idDelClienteSeleccionado;

        public string IdDelClienteSeleccionado
        {
            get { return _idDelClienteSeleccionado; }
            set { _idDelClienteSeleccionado = value; }
        }

        //Propiedad que controla la lista de clientes existentes
        //se usa como DataSource del datagridview
        private List<Cliente> _listaClientes;
        private List<Cliente> ListaClientes
        {
            get { return _listaClientes; }
            set { _listaClientes = value; }
        }
        #endregion


        private void frmBuscarCliente_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        //Carga los clientes en el DataGridView
        void CargarClientes()
        {
            ListaClientes.Clear();
            ListaClientes = ClienteBO.DevolverTodos();
            gridClientes.DataSource = null;
            gridClientes.DataSource = ListaClientes;
            gridClientes.Refresh();
        }

        //Para buscar un cliente en el datagrid, se auxilia del método BuscarEnDataGrid
        void BuscarCliente(string parametro)
        {
            switch (opcion)
            {
                case OpcionBusqueda.Id:
                    BuscarEnDataGrid("Id", parametro);
                    break;
                case OpcionBusqueda.Nombre:
                    BuscarEnDataGrid("Nombre", parametro);
                    break;
                case OpcionBusqueda.Apellido:
                    BuscarEnDataGrid("Apellido", parametro);
                    break;
                case OpcionBusqueda.Cedula:
                    BuscarEnDataGrid("Cedula", parametro);
                    break;
                default:
                    MessageBox.Show("Elija una opción de búsqueda");
                    break;
            }
        }

        void BuscarEnDataGrid(string columna,string valor)
        {
            //recorrer cada fila del datagridview
            foreach (DataGridViewRow fila in gridClientes.Rows)
            {
                //si el valor de la columna de la fila actual es igual al parámetro
                if (fila.Cells[columna].Value.ToString().Trim() == valor)
                    fila.Selected = true;//entonces seleccionarlo
                else
                    fila.Selected = false;//sino deseleccionarlo
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == radioId)//si se eligió buscar por Id
                opcion = OpcionBusqueda.Id; //seleccionar opcion de Id y asi sucesivamente
            else if (sender == radioNombre)
                opcion = OpcionBusqueda.Nombre;
            else if (sender == radioApellido)
                opcion = OpcionBusqueda.Apellido;
            else //Sino es que se eligió por cédula
                opcion = OpcionBusqueda.Cedula;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarCliente(txtBusqueda.Text);
        }

        private void btnCargarClientes_Click(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void gridClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)// si RowIndex es -1 es porque no se eligió una fila
                return;
            else
            {
                IdDelClienteSeleccionado = Convert.ToString(gridClientes.Rows[e.RowIndex].Cells[0].Value);
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }

        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridClientes.SelectedCells.Count == 0)
                return;
            else
            {
                int indiceFila = gridClientes.SelectedCells[0].RowIndex;
                IdDelClienteSeleccionado = Convert.ToString(gridClientes.Rows[indiceFila].Cells[0].Value);
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


    }
}
