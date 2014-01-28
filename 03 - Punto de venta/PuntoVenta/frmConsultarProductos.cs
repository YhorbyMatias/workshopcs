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
    public partial class frmConsultarProductos : Form
    {
        public frmConsultarProductos()
        {
            InitializeComponent();
        }
        List<Producto> _listaProductos;
        private List<Producto> ListaProductos
        {
            get { return _listaProductos; }
            set { _listaProductos = value; }
        }

        private void frmConsultarProductos_Load(object sender, EventArgs e)
        {
            radioId.CheckedChanged+=new EventHandler(Comprobar);
            radioNombre.CheckedChanged+=new EventHandler(Comprobar);
            radioProveedor.CheckedChanged+=new EventHandler(Comprobar);
            radioId.Checked = true;
            var lista = ProductoBO.DevolverTodos();
            ListaProductos = (from x in lista
                               orderby x.IdProducto,x.Nombre
                               select x).ToList();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            gridProductos.DataSource = (from x in ListaProductos
                                        select new
                                        {
                                            x.IdProducto,
                                            x.Marca,
                                            x.Nombre,
                                            x.PrecioVenta,
                                            x.Proveedor,
                                            x.Categoria,
                                            x.Descripcion
                                        }).ToList();
            gridProductos.Refresh();
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ComprobarControles())
            {
                Buscar();
            }
        }

        void Validar(object sender, KeyPressEventArgs e)
        {
            int t=0;
            if (int.TryParse(e.KeyChar.ToString(), out t))
                e.Handled = false;
            else
                e.Handled = true;
        }

        void Comprobar(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }
        private void radioId_CheckedChanged(object sender, EventArgs e)
        {
            if(radioId.Checked)
                txtBuscar.KeyPress+=new KeyPressEventHandler(Validar);
            else
                txtBuscar.KeyPress-=Validar;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #region Metodos personalizados
        bool ComprobarControles()
        {
            bool resp = true;
            if(string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                resp=false;
                MessageBox.Show("No deje el campo de busqueda vacío");
            }
            return resp;
        }


        void Buscar()
        {
            if (radioId.Checked)
            {
                //var p = (from x in ListaProductos
                //         where x.IdProducto.Equals(txtBuscar.Text)
                //         select x).ToList();

                gridProductos.DataSource = (from x in ListaProductos
                                            where x.IdProducto.Equals(txtBuscar.Text)
                                            select new
                                            {
                                                x.IdProducto,
                                                x.Marca,
                                                x.Nombre,
                                                x.PrecioVenta,
                                                x.Proveedor,
                                                x.Categoria,
                                                x.Descripcion
                                            }).ToList(); ;
                gridProductos.Refresh();
            }
            if (radioProveedor.Checked)
            {
                //var p = (from x in ListaProductos
                //         where x.Proveedor.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper())
                //         select x).ToList();
                gridProductos.DataSource = (from x in ListaProductos
                                            where x.Proveedor.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper())
                                            select new
                                            {
                                                x.IdProducto,
                                                x.Marca,
                                                x.Nombre,
                                                x.PrecioVenta,
                                                x.Proveedor,
                                                x.Categoria,
                                                x.Descripcion
                                            }).ToList();
                gridProductos.Refresh();
            }
            if (radioNombre.Checked)
            {
                //var p = (from x in ListaProductos
                //         where x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper())
                //         select x).ToList();
                gridProductos.DataSource = (from x in ListaProductos
                                            where x.Nombre.ToUpper().Contains(txtBuscar.Text.ToUpper())
                                            select new
                                            {
                                                x.IdProducto,
                                                x.Marca,
                                                x.Nombre,
                                                x.PrecioVenta,
                                                x.Proveedor,
                                                x.Categoria,
                                                x.Descripcion
                                            }).ToList(); 
                gridProductos.Refresh();
            }
        }
        #endregion

        private void gridProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(e.RowIndex == -1))
            {
                string a = gridProductos.Rows[e.RowIndex].Cells[0].Value.ToString();
                Producto p = ProductoBO.BuscarPorId(a);
                frmCrearEditarProducto fcep = new frmCrearEditarProducto(p);
                fcep.ShowDialog(this);
            }

        }


        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int indiceFila = -1;
            int indiceColumna=-1;
            for (int i = 0; i < gridProductos.SelectedCells.Count; i++)
            {
                indiceFila = gridProductos.SelectedCells[i].RowIndex;
                indiceColumna=gridProductos.SelectedCells[i].ColumnIndex;
            }
            if (indiceFila == -1)
            {
                MessageBox.Show("Seleccione un elemento");
            }
            else
            {
                DataGridViewCellEventArgs a=new DataGridViewCellEventArgs(indiceColumna,indiceFila);
                gridProductos_CellContentDoubleClick(sender, a);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }


    }
}
