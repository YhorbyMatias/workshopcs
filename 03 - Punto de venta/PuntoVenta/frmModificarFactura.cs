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
using System.Configuration;

namespace PuntoVenta
{
    public partial class frmModificarFactura : Form
    {
        public frmModificarFactura()
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
        public frmModificarFactura(int folio)
            : this()
        {
            factura = new Factura();

            // como esta en modo editar factura se busca la factura
            // con el folio pasado como argumento.
            Factura = FacturaBO.BuscarPorFolio(folio);
        }

        #region Propiedades
        // 
        // Factura que se creara
        // 
        Factura factura;

        public Factura Factura
        {
            get { return factura; }
            set { factura = value; }
        }

        //
        // Propiedad para manejar la tasa de impuestos
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
        // impuestos aculumados globales
        //
        decimal _itbisAcumulados;

        public decimal ItbisAcumulados
        {
            get { return _itbisAcumulados; }
            set
            {
                _itbisAcumulados = value;
                lblItbis.Text = string.Format("ITBIS:    {0:C}", _itbisAcumulados);
                ActualizarTotal();
            }
        }

        //
        // Esto es para guardar los Ids de los productos que se eliminen
        //
        List<String> _productosAEliminar;

        public List<String> ProductosAEliminar
        {
            get { return _productosAEliminar; }
            set { _productosAEliminar = value; }
        }

        //
        // Guardara la cantidad de productos que hay en la factura
        // 
        int _cantidadDeProductoEnFactura;

        public int CantidadDeProductoEnFactura
        {
            get { return _cantidadDeProductoEnFactura; }
            set { _cantidadDeProductoEnFactura = value; }
        }


        #endregion



        void ActualizarTotal()
        {
            lblTotal.Text = string.Format("Total:    {0:C}", _subTotal + _itbisAcumulados);
        }


        private void frmModificarFactura_Load(object sender, EventArgs e)
        {
            CargarCliente();
            CargarProductos();

            //foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
            //{
            //    String idFila = fila.Cells["Id"].Value.ToString();
            //    int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
            //    dgvLineaCompra.Rows.Remove(fila);

            //    fseleccionar_ProductoSeleccionado(
            //        idFila,
            //        cantidad
            //        );
            //}

        }

        void CargarCliente()
        {
            Cliente cliente = ClienteBO.BuscarPorId(Factura.IdCliente);

            txtNombre.Text = cliente.Nombre;
            txtApellido.Text = cliente.Apellido;
            txtCedula.Text = cliente.Cedula;
            txtDireccion.Text = cliente.Direccion;
            txtEmail.Text = cliente.Email;
            txtID.Text = cliente.Id;
            txtProvincia.Text = cliente.Provincia;

        }

        void CargarProductos()
        {
            foreach (DetalleFactura detalles in Factura.Detalles)
            {
                string nombreProducto = (ProductoBO.BuscarPorId(detalles.IdProducto).Nombre);

                fseleccionar_ProductoSeleccionado(detalles.IdProducto, detalles.Cantidad);

                //dgvLineaCompra.Rows.Add(
                //    null,
                //    detalles.IdProducto,
                //    nombreProducto,
                //    detalles.PrecioUnidad,
                //    detalles.Cantidad,
                //    detalles.Cantidad * detalles.PrecioUnidad
                //    );
            }

        }

        private void btnCambiarCantidad_Click(object sender, EventArgs e)
        {
            DataGridViewRow filaActual = dgvLineaCompra.CurrentRow;
            if (filaActual != null)
            {
                DialogResult resultado = MessageBox.Show(this, "Al cambiar la cantidad se borrará cualquier descuento hecho al producto\n¿Desea continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    frmCambiarCantidadProducto fcamb = new frmCambiarCantidadProducto(filaActual.Cells[1].Value.ToString(), Convert.ToInt32(filaActual.Cells[4].Value));
                    fcamb.AlSeleccionarCantidad += new frmCambiarCantidadProducto.CantidadNuevaEventHandler(fcampro_AlSeleccionarCantidad);
                    fcamb.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto");
            }
        }

        void fcampro_AlSeleccionarCantidad(string idProducto, int cantidadNueva)
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
            //dgvLineaCompra.Rows.RemoveAt(dgvLineaCompra.CurrentRow.Index);
            dgvLineaCompra.Rows.RemoveAt(indice);

        }

        void fseleccionar_ProductoSeleccionado(string idProductoSeleccionada, int cantidadSeleccionada)
        {
            Producto productoAAgregar = ProductoBO.BuscarPorId(idProductoSeleccionada);

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
                    fila.Cells["Cantidad"].Value = tempCantidad.ToString();

                    decimal _subtotalProductoExistente = tempCantidad * productoAAgregar.PrecioVenta;

                    fila.Cells["Sub_Total"].Value = _subtotalProductoExistente.ToString();

                    //Actualizar el Subtotal global con el subtotal agregado
                    //SubTotal += cantidadSeleccionada * productoAAgregar.PrecioVenta;
                    SubTotal += _subtotalProductoExistente;

                    //Actualizar los itbis acumulados globales
                    //ItbisAcumulados += (productoAAgregar.PrecioVenta * cantidadSeleccionada) * Impuestos;
                    ItbisAcumulados += _subtotalProductoExistente * Impuestos;

                    //el return es para parar la ejecución aquí, si no se pone return
                    //entonces se duplicará el producto en el DataGridView
                    return;
                }
            }

            //Agregando el producto en el DataGridView 
            dgvLineaCompra.Rows.Add(
                null,
                productoAAgregar.IdProducto,
                productoAAgregar.Nombre,
                productoAAgregar.PrecioVenta,
                cantidadSeleccionada,
                productoAAgregar.PrecioVenta * cantidadSeleccionada);
            //SubTotal += (productoAAgregar.PrecioVenta * cantidadSeleccionada);
            int indiceFila = 0;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmSeleccionarProducto fseleccionar = new frmSeleccionarProducto();
            fseleccionar.ProductoSeleccionado += new frmSeleccionarProducto.SeleccionadoEventHandler(fseleccionar_ProductoSeleccionado);
            fseleccionar.ShowDialog();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!Comprobar())
                return;
            DialogResult resultado = MessageBox.Show(this, "Estás seguro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {

                #region Crear Encabezado de factura
                Factura factura = new Factura();
                factura.IdCliente = Factura.IdCliente;
                factura.Fecha = Factura.Fecha;
                factura.Direccion = txtDireccionFacturacion.Text;

                factura.UserName = (Sesion.UsuarioActual != null ? Sesion.UsuarioActual.UserName : "Admin");

                foreach (DataGridViewRow fila in dgvLineaCompra.Rows)
                {
                    DetalleFactura detalleFactura = new DetalleFactura();
                    detalleFactura.IdProducto = Convert.ToString(fila.Cells["Id"].Value);
                    detalleFactura.PrecioUnidad = Convert.ToDecimal(fila.Cells["PrecioUnidad"].Value);
                    detalleFactura.Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);

                    factura.Detalles.Add(detalleFactura);
                }
                factura.ImpuestosAcumulados = ItbisAcumulados;

                FacturaBO.ActualizarFactura(factura);

                MessageBox.Show("Hecho");

                this.Dispose();
                #endregion
            }
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
    }
}
