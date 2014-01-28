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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        //Evento para avisar al formulario del Login que se cerró
        //Si se manda true es para cerrar el programa, si se manda false o nada se cierra sesión
        public delegate void AlCerrarseEventHandler(bool valor = false);

        public event AlCerrarseEventHandler AlCerrarse;

        /*-----BOTÓN DE CONFIGURAR USUARIOS----
         * */
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurarUsuario fcu = new frmConfigurarUsuario();
            fcu.MdiParent = this;
            fcu.Show();
        }



        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //timer1.Start();
            statusStrip1.Items[0].Text += Sesion.UsuarioActual.UserName;
            statusStrip1.Items[2].Text += DateTime.Now.ToShortDateString();

            if (!Sesion.UsuarioActual.Consultar)
                consultasToolStripMenuItem.Enabled = false;

            if (!Sesion.UsuarioActual.Administrar)
            {
                for (int i = 0; i < toolStrip1.Items.Count; i++)
                {
                    toolStrip1.Items[i].Enabled = false;
                    if (toolStrip1.Items[i] == cerrarSesiónToolStripMenuItem)
                        toolStrip1.Items[i].Enabled = true;
                }
            }

            if (!Sesion.UsuarioActual.Reportes)
                reportesToolStripMenuItem.Enabled = false;


        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Para evitar cerrar sin querer
            DialogResult result = MessageBox.Show("¿Está seguro de que desea salir?", "¿Está seguro", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                AlCerrarse(true);
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusStrip1.Items[2].Text = "Fecha: " + DateTime.Now.ToShortDateString() + " Hora: " + DateTime.Now.ToShortTimeString();
        }

        private void modificarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigurarUsuario fconfu = new frmConfigurarUsuario();
                fconfu.MdiParent = this;
                fconfu.Show();
        }



        private void eliminarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEliminarUsuario felim = new frmEliminarUsuario();
            felim.MdiParent = this;
            felim.Show();
        }

        private void crearUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCrearUsuario fcrear = new frmCrearUsuario();
            fcrear.MdiParent = this;
            fcrear.Show();
        }

        private void nuevoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoCliente fnc = new frmNuevoCliente();
            fnc.MdiParent = this;
            fnc.Show();
        }
        private void consultarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarProductos fcp = new frmConsultarProductos();
            fcp.ShowDialog(this);
        }

        private void nuevaCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevaCategoria fnc = new frmNuevaCategoria();
            fnc.ShowDialog(this);
        }

        private void modificarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmModificarCliente fmd = new frmModificarCliente();
            fmd.MdiParent = this;
            fmd.Show();
        }

        private void movimientoDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRptProductosPorFecha frept = new frmRptProductosPorFecha();
            frept.ShowDialog(this);
        }

        private void nuevoProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCrearEditarProducto fcrearpro = new frmCrearEditarProducto();
            fcrearpro.ShowDialog(this);
        }

        private void cerrarSesiónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Para evitar cerrar sesión de forma accidental
            DialogResult resultado = MessageBox.Show("¿Desea cerrar sesión?", "Cerrando sesión", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                AlCerrarse(false);
                this.Dispose();
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracionPrograma fconfpro = new frmConfiguracionPrograma();
            fconfpro.MdiParent = this;
            fconfpro.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRptProveedores frmProv = new frmRptProveedores();
            frmProv.MdiParent = this;
            frmProv.Show();
        }

        private void editarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarProductos fconseditar = new frmConsultarProductos();
            fconseditar.MdiParent = this;
            fconseditar.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcercaDe fac = new frmAcercaDe();
                fac.MdiParent = this;
                fac.Show();
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEliminarProducto felpr = new frmEliminarProducto();
            felpr.MdiParent = this;
            felpr.Show();
        }

        private void agregarAlAlmacenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExistencias frmAgregarExistencias = new frmExistencias();
            frmAgregarExistencias.ShowDialog(this);
        }

        private void corregirExistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExistencias frmcorregirExistencias = new frmExistencias(true);
            frmcorregirExistencias.ShowDialog();
        }

        private void nuevaMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevaMarca fnmarca = new frmNuevaMarca();
            fnmarca.MdiParent = this;
            fnmarca.Show();
        }

        private void editarMarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaMarcas flistmarcs = new frmListaMarcas();
            flistmarcs.MdiParent = this;
            flistmarcs.Show();
        }

        private void nuevaVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompraDetalle fcomp = new frmCompraDetalle();
            fcomp.MdiParent = this;
            fcomp.Show();
        }

        private void deshacerVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuscarFactura fbusca = new frmBuscarFactura();
            fbusca.MdiParent = this;
            fbusca.Show();
        }

        private void consultarFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void porDíasOMesesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaGanancia fcons = new frmConsultaGanancia();
            fcons.MdiParent = this;
            fcons.Show();
        }

        private void nuevaVentaAlPorMayorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompraDetalle fcompalpormayor = new frmCompraDetalle(true);
            fcompalpormayor.MdiParent = this;
            fcompalpormayor.Show();
        }

        private void nuevaMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoProveedor fnpr = new frmNuevoProveedor();
            fnpr.MdiParent = this;
            fnpr.Show();
        }

        private void editarMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSeleccionarProveedor fsel = new frmSeleccionarProveedor();
            fsel.MdiParent = this;
            fsel.Show();
        }




    }
}
