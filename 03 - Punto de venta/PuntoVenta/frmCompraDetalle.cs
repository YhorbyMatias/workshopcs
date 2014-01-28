/**
 *    Para Proyecto III
 * 
 * */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using PuntoVenta.Entidades;
using Microsoft.Reporting.WinForms;
using PuntoVenta.CapaNegocios;
namespace PuntoVenta
{
    public partial class frmCompraDetalle : Form
    {
        public frmCompraDetalle()
        {
            InitializeComponent();
            //Desactivar el botón de modificar cliente porque aún no se ha seleccionado uno
            btnModificar.Enabled = false;
            //Leer los impuestos desde el archivo de configuración
            Impuestos = Convert.ToDecimal(ConfigurationManager.AppSettings["Impuestos"].ToString());

            //Inicializar las variables de subtotal e itbis acumulados
            SubTotal = 0M;
            ItbisAcumulados = 0M;
        }
        public frmCompraDetalle(bool modoMayoreo)
            : this()
        {
            this.modoMayoreo = modoMayoreo;

            lblAlPorMayor.Visible = true;
        }
        bool modoMayoreo;

        #region Propiedades

        //
        // Propiedad para manejar los impuestos
        //
        decimal _impuestos;

        public decimal Impuestos
        {
            get { return _impuestos; }
            private set { _impuestos = value; }
        }

        //
        // Guarda el subtotal global
        //
        decimal _subTotal;

        public decimal SubTotal
        {
            get { return _subTotal; }
            set
            {
                _subTotal = value;
                lblSubtotal.Text = string.Format("Subtotal\nGlobal : {0:C}", _subTotal);
                ActualizarTotal();
            }
        }

        //
        // itbis aculumados globales
        //
        decimal _itbisAcumulados;

        public decimal ItbisAcumulados
        {
            get { return _itbisAcumulados; }
            set { _itbisAcumulados = value;
            lblItbis.Text = string.Format("ITBIS:    {0:C}", _itbisAcumulados);
            ActualizarTotal();
            }
        }

        #endregion

        void ActualizarTotal()
        {
            lblTotal.Text = string.Format("Total:    {0:C}", _subTotal + _itbisAcumulados);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscar el cliente a venderle productos
            frmBuscarCliente fbuscarCliente = new frmBuscarCliente();
            if (fbuscarCliente.ShowDialog(this) == DialogResult.OK)
            {
                Cliente cliente = ClienteBO.BuscarPorId(fbuscarCliente.IdDelClienteSeleccionado);
                RellenarDatosCliente(cliente);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoCliente frmnuevoCliente = new frmNuevoCliente();
            DialogResult resultado = frmnuevoCliente.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                Cliente cliente = ClienteBO.BuscarPorId(frmnuevoCliente.IdClienteCreado);
                RellenarDatosCliente(cliente);
            }

        }

        void RellenarDatosCliente(Cliente cliente)
        {
            txtID.Text = cliente.Id;
            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtCedula.Text = cliente.Cedula;
            txtDireccion.Text = cliente.Direccion;
            txtEmail.Text = cliente.Email;
            txtProvincia.Text = cliente.Provincia;

            btnModificar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Cliente clienteAModificar = ClienteBO.BuscarPorId(txtID.Text);
            frmNuevoCliente frmmodfclient = new frmNuevoCliente(clienteAModificar);
            if (frmmodfclient.ShowDialog() == DialogResult.OK)
            {
                Cliente clientemodificado = ClienteBO.BuscarPorId(txtID.Text);
                RellenarDatosCliente(clientemodificado);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmSeleccionarProducto fseleccionar = new frmSeleccionarProducto();
            fseleccionar.ProductoSeleccionado += new frmSeleccionarProducto.SeleccionadoEventHandler(fseleccionar_ProductoSeleccionado);
            fseleccionar.ShowDialog();
        }

        //Manejador del evento 
        void fseleccionar_ProductoSeleccionado(string idProductoSeleccionada, int cantidadSeleccionada)
        {
            Producto productoAAgregar = ProductoBO.BuscarPorId(idProductoSeleccionada);

            //recorrer las filas a ver si existe el producto
            foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
            {
                //Si existe un producto en el DataGridView con el mismo Id
                //que el producto que estoy agregando
                if (fila.Cells["Id"].Value.ToString() == productoAAgregar.IdProducto)
                {
                    //Suma la cantidad que seleccionada a la que hay en el DataGridView
                    int tempCantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value) + cantidadSeleccionada;
                    
                    //Si la suma de las cantidades es mayor a las existencias del producto en el almacén
                    //Entonces lanzar una excepción
                    if (tempCantidad > productoAAgregar.Cantidad)
                        throw new Exception("No puedes agregar más unidades, la cantidad \nsobrepasa las existencias disponibles");

                    //Actualizar el campo Cantidad en el DataGridView
                    //fila.Cells["Cantidad"].Value = tempCantidad.ToString();

                    cantidadSeleccionada = tempCantidad;

                    decimal _subtotalProductoExistente = tempCantidad * productoAAgregar.PrecioVenta;

                    //fila.Cells["Sub_Total"].Value = _subtotalProductoExistente.ToString();

                    //Actualizar el Subtotal global con el subtotal agregado
                    //SubTotal += cantidadSeleccionada * productoAAgregar.PrecioVenta;
                    //SubTotal += _subtotalProductoExistente;

                    //Actualizar los itbis acumulados globales
                    //ItbisAcumulados += (productoAAgregar.PrecioVenta * cantidadSeleccionada) * Impuestos;
                    //ItbisAcumulados += _subtotalProductoExistente * Impuestos;

                    //el return es para parar la ejecución aquí, si no se pone return
                    //entonces se duplicará el producto en el DataGridView
                    //return;
                    EliminarProductoActualDelDataGrid(fila.Index);
                }
            }
            
            //Agregando el producto en el DataGridView 
            dgvLineaCompra.Rows.Add(
                null,
                productoAAgregar.IdProducto,
                productoAAgregar.Nombre,
                productoAAgregar.PrecioVenta,
                cantidadSeleccionada,
                productoAAgregar.PrecioVenta*cantidadSeleccionada);
            //SubTotal += (productoAAgregar.PrecioVenta * cantidadSeleccionada);
            int indiceFila=0;
            foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
            {
                if (fila.Cells["Id"].Value.ToString() == productoAAgregar.IdProducto)
                    indiceFila = fila.Index;
            }
            decimal _subtotalProductoAgregado = Convert.ToDecimal(dgvLineaCompra.Rows[indiceFila].Cells["Sub_Total"].Value);
            SubTotal += _subtotalProductoAgregado;

            //ItbisAcumulados += (productoAAgregar.PrecioVenta * cantidadSeleccionada) * Impuestos;

            ItbisAcumulados += _subtotalProductoAgregado * Impuestos;
        }

        private void frmCompraDetalle_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn botonColumna = dgvLineaCompra.Columns[0] as DataGridViewButtonColumn;
        }

        private void dgvLineaCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Cuardo el índice de la fila donde hice click
            int filaSeleccionada = e.RowIndex;

            //si el índice es 0 (El botón en el DataGridView)
            if (e.ColumnIndex == 0)
            {
                //Mostrar el mensaje para confirmar
                DialogResult resultado = MessageBox.Show(
                    this,
                    string.Format(@"¿Estás seguro de que desea eliminar este producto: {0} ?", dgvLineaCompra.Rows[filaSeleccionada].Cells[2].Value.ToString()),
                    "Aviso",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                //Si se seleccionó que Sí
                if (resultado == DialogResult.Yes)
                {
                    EliminarProductoActualDelDataGrid(e.RowIndex);
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!Comprobar())
                return;
            //DialogResult resultado = MessageBox.Show(this, "Estás seguro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //DialogResult resultado=frmCambio 

            //if (resultado == DialogResult.Yes)
            //{

                #region Crear Encabezado de factura
                Factura factura = new Factura();
                factura.IdCliente = txtID.Text;
                factura.Fecha = DateTime.Now.Date;
                factura.Direccion = txtDireccionFacturacion.Text;

                factura.UserName = (Sesion.UsuarioActual != null ? Sesion.UsuarioActual.UserName : "Admin");

                foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
                {
                    DetalleFactura detalleFactura = new DetalleFactura();
                    detalleFactura.IdProducto = Convert.ToString(fila.Cells["Id"].Value);

                    detalleFactura.PrecioUnidad = Convert.ToDecimal(fila.Cells["PrecioUnidad"].Value);

                    if (modoMayoreo)
                        detalleFactura.PrecioUnidad -= (detalleFactura.PrecioUnidad * 0.10M);

                    detalleFactura.Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                    
                    factura.Detalles.Add(detalleFactura);
                }
                factura.ImpuestosAcumulados = ItbisAcumulados;

                frmCambio fcamb = new frmCambio();

                fcamb.Total = factura.Total;

                DialogResult result = fcamb.ShowDialog();

                List<ReportParameter> listaParametros = new List<ReportParameter>();

                if (result == DialogResult.OK)
                {
                    int folio = FacturaBO.RegistrarFacturacion(factura);

                    frmMostrarFactura fmost = new frmMostrarFactura(folio);

                    ReportParameter prmEmpresa = new ReportParameter
                    (
                        "Empresa", ConfigurationManager.AppSettings["Empresa"].ToString()
                    );
                    listaParametros.Add(prmEmpresa);

                    ReportParameter prmDireccionEmpresa = new ReportParameter
                    (
                        "DireccionEmpresa", ConfigurationManager.AppSettings["Direccion"].ToString()
                    );
                    listaParametros.Add(prmDireccionEmpresa);

                    ReportParameter prmTelefonoEmpresa = new ReportParameter
                    (
                        "TelefonoEmpresa",
                        ConfigurationManager.AppSettings["Telefono"].ToString()
                    );
                    listaParametros.Add(prmTelefonoEmpresa);

                    ReportParameter prmRNC = new ReportParameter
                    (
                        "RNC",
                        ConfigurationManager.AppSettings["RNC"].ToString()
                    );
                    listaParametros.Add(prmRNC);

                    ReportParameter prmFecha = new ReportParameter
                    (
                        "Fecha",
                        DateTime.Today.ToString()
                    );
                    listaParametros.Add(prmFecha);

                    ReportParameter prmCliente = new ReportParameter
                    (
                        "Cliente",
                        txtNombre.Text
                    );
                    listaParametros.Add(prmCliente);

                    ReportParameter prmDireccionCliente = new ReportParameter
                    (
                        "DireccionCliente",
                        txtDireccion.Text
                    );
                    listaParametros.Add(prmDireccionCliente);

                    ReportParameter prmNumeroFactura = new ReportParameter
                    (
                        "NumeroFactura",
                        folio.ToString()
                    );
                    listaParametros.Add(prmNumeroFactura);

                    ReportParameter prmTotalFactura = new ReportParameter
                    (
                        "TotalFactura",
                        factura.Total.ToString()
                    );
                    listaParametros.Add(prmTotalFactura);

                    ReportParameter prmImpuestos = new ReportParameter
                    (
                        "Impuestos",
                        factura.ImpuestosAcumulados.ToString()
                    );
                    listaParametros.Add(prmImpuestos);

                    ReportParameter prmSubTotal = new ReportParameter
                    (
                        "SubTotal",
                        SubTotal.ToString()
                    );
                    listaParametros.Add(prmSubTotal);

                    fmost.reportViewer1.LocalReport.SetParameters(listaParametros);
                    fmost.reportViewer1.RefreshReport();
                    fmost.ShowDialog();

                    MessageBox.Show("Hecho");


                    this.Dispose();
                }


                #endregion

            //}
        }


        bool Comprobar()
        {
            bool resul = true;
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Tiene que seleccionar un cliente");
                resul = false;
            }

            if (dgvLineaCompra.Rows.Count <= 0)
            {
                MessageBox.Show("No ha agregado productos");
                resul = false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccionFacturacion.Text))
            {
                resul = false;
                errorProvider1.Clear();
                errorProvider1.SetError(txtDireccionFacturacion, "Escriba una dirección");
            }
            return resul;
        }

        private void btnCambiarCantidad_Click(object sender, EventArgs e)
        {
            DataGridViewRow filaActual = dgvLineaCompra.CurrentRow;
            if (filaActual != null)
            {
                DialogResult resultado = MessageBox.Show(this,"Al cambiar la cantidad se borrará cualquier descuento hecho al producto\n¿Desea continuar?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    frmCambiarCantidadProducto fcamb = new frmCambiarCantidadProducto(filaActual.Cells[1].Value.ToString(), Convert.ToInt32(filaActual.Cells[4].Value));
                    fcamb.AlSeleccionarCantidad += new frmCambiarCantidadProducto.CantidadNuevaEventHandler(fcamb_AlSeleccionarCantidad);
                    fcamb.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
        }


        void fcamb_AlSeleccionarCantidad(string idProducto, int cantidadNueva)
        {
            //dgvLineaCompra.Rows.Remove(dgvLineaCompra.CurrentRow);

            foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
            {
                if (fila.Cells["Id"].Value.ToString() == idProducto)
                {
                    EliminarProductoActualDelDataGrid(fila.Index);
                }

            }
            //EliminarProductoActualDelDataGrid();

            fseleccionar_ProductoSeleccionado(idProducto, cantidadNueva);

        }


        void EliminarProductoActualDelDataGrid(int indice)
        {
            //guardar el subtotal de la del producto a eliminar
            decimal subtotalAEliminar = Convert.ToDecimal(dgvLineaCompra.Rows[indice].Cells["PrecioUnidad"].Value) * Convert.ToInt32(dgvLineaCompra.Rows[indice].Cells["Cantidad"].Value);
            //Restarlo al subtotal
            SubTotal -= subtotalAEliminar;
            //Retarle los itbis a los itbis acumulados
            ItbisAcumulados -= subtotalAEliminar * Impuestos;

            //Actualizar la información
            lblSubtotal.Text = string.Format("SubTotal: {0:C}", SubTotal);
            lblItbis.Text = string.Format("ITBIS: {0:C}", ItbisAcumulados);
            lblTotal.Text = string.Format("Total: {0:C}", SubTotal + ItbisAcumulados);

            //Elimino la fila
            dgvLineaCompra.Rows.RemoveAt(dgvLineaCompra.CurrentRow.Index);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtDireccionFacturacion.Text = txtDireccion.Text;
        }


    }

}
