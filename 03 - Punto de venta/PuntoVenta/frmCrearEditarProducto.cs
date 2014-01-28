using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PuntoVenta.Entidades;
using PuntoVenta.CapaNegocios;
using System.Configuration;

namespace PuntoVenta
{
    public partial class frmCrearEditarProducto : Form
    {

        #region Constructores
        public frmCrearEditarProducto()
        {
            InitializeComponent();
            DirectorioFotos = Directory.GetCurrentDirectory() + @"\Imagenes";
            if (!Directory.Exists(DirectorioFotos))
            {
                Directory.CreateDirectory(DirectorioFotos);
            }
            Ganancia = Convert.ToDecimal(ConfigurationManager.AppSettings["Ganancia"]);
        }
        //
        // Para controlar la variable ganancia
        //
        decimal _ganancia;

        public decimal Ganancia
        {
            get { return _ganancia; }
            set { _ganancia = value; }
        }

        private string _directorioFotos;
        private string DirectorioFotos
        {
            get { return _directorioFotos; }
            set { _directorioFotos = value; }
        }
        //Este constructor se usará cuando se vaya a editar un producto
        public frmCrearEditarProducto(Producto pro)
            : this()
        {
            ProductoEditar = pro;

            _modoEditar = true;//activar el modo editar

            //Como está en modo editar el Id no se podrá modificar:
            txtId.ReadOnly = true;
            chkGenerarId.Enabled = false;

            //Desactivamos el nudCantidad
            nudCantidad.Enabled = false;

            pbFotoProducto.ImageLocation = DirectorioFotos + @"\" + pro.Foto;
        }
        #endregion


        #region Propiedades
        //Propiedad para manejar el producto a editar mandado como parámetro
        Producto _productoEditar;

        private Producto ProductoEditar
        {
            get { return _productoEditar; }
            set { _productoEditar = value; }
        }


        //Propiedad para guardar el nombre de la foto seleccionada en el picture box
        private string _nombreFoto = "";

        protected string NombreFoto
        {
            get { return _nombreFoto; }
            set { _nombreFoto = value; }
        }

        #endregion

        #region Campos
        //Estos campos lista guardarán los datos traídos desde
        //la base de datos
        List<Categoria> _listaCategorias;
        List<UnidadMedida> _listaUnidadesMedidas;
        List<Proveedor> _listaProveedores;
        List<Marca> _listaMarcas;

        bool _modoEditar;//se activa si se está editando un producto

        Producto pro;//manejar el producto que se creará

        #endregion

        private void frmCrearEditarProducto_Load(object sender, EventArgs e)
        {
            //Lleno cada combobox con sus datos correspondientes
            CargarCategorias();
            CargarMarcas();
            CargarUnidadesMedida();
            CargarProveedores();
            if (_modoEditar)
                CargarControlesModoEdicion();
        }




        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult resul = MessageBox.Show("¿Está seguro?", "Aviso", MessageBoxButtons.YesNo);

            if ((resul == DialogResult.Yes) & ComprobarControles())//Si se eligió Sí y los controles no están vacíos
            {
                CrearProducto();
                this.Dispose();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            frmNuevaMarca fnm = new frmNuevaMarca();
            DialogResult resultado = fnm.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                CargarMarcas();

                for (int i = 0; i < cboMarca.Items.Count; i++)
                {
                    if (cboMarca.Items[i].ToString() == fnm.MarcaCreada.Descripcion)
                    {
                        cboMarca.SelectedIndex = i;
                    }
                }
            }
            //fnm.AlCrear += new frmNuevaMarca.CrearEventHandler(fnm_AlCrearMarca);
            //fnm.ShowDialog(this);
        }

        private void chkGenerarId_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkGenerarId.Checked)
                txtId.ReadOnly = true;
            else
                txtId.ReadOnly = false;
        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.JPG| PNG |*.PNG";
            DialogResult resultado = ofd.ShowDialog(this);
            if (resultado == DialogResult.OK)
            {
                pbFotoProducto.ImageLocation = ofd.FileName;
                NombreFoto = ofd.SafeFileName;
            }
        }

        #region Métodos personalizados
        //
        // Para crear un producto nuevo
        //
        void CrearProducto()
        {

            pro = new Producto();
            pro.Nombre = txtNombreProducto.Text;
            pro.PrecioCompra = Convert.ToDecimal(nudPrecioCompra.Value);
            pro.PrecioVenta = Convert.ToDecimal(nudPrecioVenta.Value);
            pro.Categoria = (Categoria)cboCategoria.SelectedItem;
            pro.Proveedor = (Proveedor)cboProveedores.SelectedItem;
            pro.UnidadMedida = (UnidadMedida)cboUnidadMedida.SelectedItem;
            pro.Cantidad = (int)nudCantidad.Value;
            pro.SetUsuarioResponsable(Sesion.UsuarioActual);
            pro.Foto = NombreFoto;
            pro.Descripcion = txtDescripcion.Text;
            pro.CantidadMayoreo = 0; //(int)nudCantidadMayoreo.Value;
            pro.Marca = (Marca)cboMarca.SelectedItem;

            // Para comprobar si esta en el modo de editar un producto
            if (_modoEditar)
            {
                pro.IdProducto = txtId.Text;
                ProductoBO.Actualizar(pro);
                if (File.Exists(pbFotoProducto.ImageLocation))
                {
                    File.Copy(pbFotoProducto.ImageLocation, DirectorioFotos + @"\" + NombreFoto);
                }
            }
            else
            {
                if (chkGenerarId.Checked)//si el checkbox de generar id está seleccionado
                {
                    GenerarId(); //Hacer uno, en caso contrario se usa el del txtIdProducto
                }

                pro.IdProducto = txtId.Text;

                //Le asigno a resultado lo que devuelve el método crear
                bool resultado = ProductoBO.Crear(pro);


                if (resultado)// si se creó==true, sino false
                {
                    MessageBox.Show("Creado");
                    if (pbFotoProducto.Image != null)
                    {
                        File.Copy(pbFotoProducto.ImageLocation, DirectorioFotos + @"\" + NombreFoto);
                    }
                }
                else
                    MessageBox.Show("No se pudo crear");
            }
        }
        /// <summary>
        /// Genera un Código aleatorio.
        /// </summary>
        private void GenerarId()
        {
            StringBuilder sb = new StringBuilder();

            string palabra = txtNombreProducto.Text;//guardo en una variable la oracion 

            palabra.QuitarEspacios();//le quito los espacios

            int cantidadCaracteres = palabra.Count();//guardo la cantidad de caracteres

            palabra.Remove(cantidadCaracteres / 2);//Elimino las letras desde la mitad

            sb.Append(palabra);//lo agrego a stringbuilder

            Random rnd = new Random();//para generar números aleatorios

            sb.Append(rnd.Next(DateTime.Now.Millisecond));//lo genero

            txtId.Text = sb.ToString();//se lo asigno al txtId
        }


        //Para que no pongan letras en los textbox
        //void FiltroTextBox(object sender, KeyPressEventArgs e)
        //{
        //    bool resp = true;
        //    if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete)
        //        resp = false;
        //    if (char.IsNumber(e.KeyChar))
        //        resp = false;
        //    e.Handled = resp;
        //}


        /// <summary>
        /// Comprueba que cada control esté rellenado
        /// </summary>
        /// <returns></returns>
        bool ComprobarControles()
        {
            bool ok = true;
            if (cboMarca.SelectedValue == null)
            {
                error.SetError(cboMarca, "Seleccione la Marca");
                ok = false;
            }
            if (cboCategoria.SelectedValue == null)
            {
                error.SetError(cboCategoria, "Seleccione una categoría");
                ok = false;
            }
            if (cboUnidadMedida.SelectedValue == null)
            {
                error.SetError(cboUnidadMedida, "Seleccione una unidad de medida");
                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
            {
                error.SetError(txtNombreProducto, "Escriba un nombre de producto");
                ok = false;
            }
            if (nudPrecioCompra.Value <= 0 | nudPrecioVenta.Value <= 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append(" el precio de compra y precio de venta no deben estar en 0");

                error.SetError(nudPrecioCompra, sb.ToString());
                error.SetError(nudPrecioVenta, sb.ToString());

                ok = false;
            }

            //if (nudPrecioVenta.Value <= nudPrecioCompra.Value)
            //{
            //    MessageBox.Show("El precio de venta no debe ser menor o igual\n al precio de compra.");
            //    ok = false;
            //}

            if (chkGenerarId.Checked == false && string.IsNullOrWhiteSpace(txtId.Text))
            {
                error.SetError(txtId, "No deje el Id vacío, o sino seleccione 'Generar'");

                ok = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                ok = false;

                error.SetError(txtDescripcion, "Escriba una descripcion");
            }
            if (ok)
                error.Clear();
            return ok;
        }

        ////
        //// Se ejecutará cuando se llame al evento AlCrear del frmMarca
        //// Osea, cuando se cree una marca nueva
        //void fnm_AlCrearMarca()
        //{
        //    CargarMarcas();

        //    for (int i = 0; i < cboMarca.Items.Count; i++)
        //    {
        //        if(cboMarca.Items[i].ToString()==
        //    }
        //}
        #endregion


        #region Métodos de carga
        void CargarMarcas()
        {
            /*
             * Cargando las marcas
             * **/
            _listaMarcas = MarcaBO.DevolverTodos();
            cboMarca.DataSource = _listaMarcas;
            cboMarca.DisplayMember = "Descripcion";
        }

        void CargarCategorias()
        {
            /*
            //Cargando las categorías
             * 
             * */
            _listaCategorias = CategoriaBO.DevolverTodos();
            cboCategoria.DataSource = _listaCategorias;
            cboCategoria.ValueMember = "Descripcion";
            cboCategoria.DisplayMember = "Descripcion";

        }

        void CargarUnidadesMedida()
        {
            /*
             * Cargando las unidades de medida
             * */
            _listaUnidadesMedidas = UnidadMedidaBO.DevolverTodos();
            cboUnidadMedida.DataSource = _listaUnidadesMedidas;
            cboUnidadMedida.ValueMember = "Descripcion";
            cboUnidadMedida.DisplayMember = "Descripcion";

        }

        void CargarProveedores()
        {
            /*
             * Cargando proveedores
             * 
             * */
            _listaProveedores = ProveedorBO.DevolverTodos();
            cboProveedores.DataSource = _listaProveedores;
            cboProveedores.ValueMember = "Nombre";
            cboProveedores.DisplayMember = "Nombre";

        }

        //Esto es para cuando se va a editar un prodcuto
        void CargarControlesModoEdicion()
        {

            txtId.Text = ProductoEditar.IdProducto;
            txtDescripcion.Text = ProductoEditar.Descripcion;
            txtNombreProducto.Text = ProductoEditar.Nombre;
            Establecer(ref cboCategoria, ProductoEditar.Categoria.Descripcion);
            Establecer(ref cboMarca, ProductoEditar.Marca.Descripcion);
            Establecer(ref cboProveedores, ProductoEditar.Proveedor.Nombre);
            Establecer(ref cboUnidadMedida, ProductoEditar.UnidadMedida.Descripcion);
            nudCantidad.Value = (decimal)ProductoEditar.Cantidad;
            nudCantidadMayoreo.Value = 0M;// (decimal)ProductoEditar.CantidadMayoreo;
            nudPrecioCompra.Value = (decimal)ProductoEditar.PrecioCompra;
            nudPrecioVenta.Value = (decimal)ProductoEditar.PrecioVenta;
        }
        /// <summary>
        ///  Sirve para poner el mismo valor de los comboBox con el valor de lo 
        ///  que tiene el producto a editar
        /// </summary>
        /// <param name="combobox">Control o variable de tipo comboBox</param>
        /// <param name="parametro">El valor a buscar para asignarle al comboBox como elemento actual</param>
        public void Establecer(ref ComboBox combobox, string parametro)
        {
            int indice = 0;//indice 0, si hubo un error será seleccionado el primer elemento del comboBox
            for (int i = 0; i < combobox.Items.Count; i++)//recorriendo los items del comboBox
            {
                if (combobox.Items[i].ToString().Contains(parametro))//si el texto o valor del item es igual al parametro
                {
                    indice = i;//asignar a la variable índice el indice del valor encontrado
                }
            }
            combobox.SelectedIndex = indice;//aquí ponemos que el elemento actual sea el del indice encontrado
        }
        #endregion


        private void nudPrecioCompra_ValueChanged(object sender, EventArgs e)
        {
            //Calcular el precioventa de la forma siguiente PrecioCompra * Ganancia,
            //este resultado se le suma al precioCompra
            nudPrecioVenta.Value = (nudPrecioCompra.Value * Ganancia) + nudPrecioCompra.Value;
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            frmNuevaCategoria fn = new frmNuevaCategoria();
            DialogResult resultado = fn.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                CargarCategorias();

                for (int i = 0; i < cboCategoria.Items.Count; i++)
                {
                    if (cboCategoria.Items[i].ToString() == fn.CategoriaCreada.Descripcion)
                    {
                        cboCategoria.SelectedIndex = i;
                    }
                }

            }
        }

        private void btnNuevaUnidadMedida_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            frmNuevoProveedor fn = new frmNuevoProveedor();

            DialogResult resultado = fn.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                CargarProveedores();

                for (int i = 0; i < cboProveedores.Items.Count; i++)
                {
                    if (cboProveedores.Items[i].ToString() == fn.ProveedorCreado.Nombre)
                    {
                        cboProveedores.SelectedIndex = i;
                    }
                }
            }
        }


        }


        public static class ClaseString
        {
            /// <summary>
            /// Quita los espacios en blanco de las palabras
            /// </summary>
            /// <param name="oracion"></param>
            /// <returns></returns>
            public static string QuitarEspacios(this string oracion)
            {
                StringBuilder sb = new StringBuilder();
                string[] coleccion = oracion.Split(' ');
                foreach (string palabra in coleccion)
                {
                    sb.Append(palabra);
                }
                return sb.ToString();
            }


        }
    }

