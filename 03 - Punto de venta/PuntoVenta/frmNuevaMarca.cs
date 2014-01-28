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
    public partial class frmNuevaMarca : Form
    {
        public frmNuevaMarca()
        {
            InitializeComponent();
            CancelButton = btnCancelar;
        }

        //Constructor para cuando se va a modificar una marca
        public frmNuevaMarca(Marca _marca)
        {
            InitializeComponent();
            CancelButton = btnCancelar;
            Marca = _marca;
            txtId.Text = Marca.Id.ToString();
            txtDescripcion.Text = Marca.Descripcion;
            ModoEditar = true;
        }

        bool _ModoEditar;

        private bool ModoEditar
        {
            get { return _ModoEditar; }
            set { _ModoEditar = value; }
        }

        private Marca _marca;

        private Marca Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }


        public Marca MarcaCreada { get; set; }  

        void CrearMarca()
        {
            Marca _marca = new Marca();
            _marca.Descripcion = txtDescripcion.Text;
            _marca.Id = MarcaBO.Crear(_marca);

            MarcaCreada = _marca;

            if (_marca.Id > 0)
            {
                MessageBox.Show("Marca creada satisfactoriamente");
                if (AlCrear != null)
                    AlCrear();
            }
            else
            {
                MessageBox.Show("Error no se pudo crear la marca");
            }
        }

        void ModificarMarca()
        {
            Marca m = new Marca();
            m.Id = Convert.ToInt32(txtId.Text);
            m.Descripcion = txtDescripcion.Text;
            bool resultado = MarcaBO.Modificar(m);
            if (resultado)
            {
                MessageBox.Show("Modificado");
                if (AlModificar != null)
                    AlModificar();
            }
            else
                MessageBox.Show("Error, no se pudo modificar");
        }

        public delegate void CrearEventHandler();
        public event CrearEventHandler AlCrear;

        public delegate void ModificarEventHandler();
        public event ModificarEventHandler AlModificar;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool ExisteMarca()
        {
            bool existe = false;
            var listaMarcas = MarcaBO.DevolverTodos();
            var marca = (from x in listaMarcas
                         where x.Descripcion.Contains(txtDescripcion.Text)
                         select x).SingleOrDefault();
            if (marca != null)
            {
                MessageBox.Show("La marca existe");
                existe = true;
            }
            return existe;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro", "Aviso", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                if (ModoEditar)
                    ModificarMarca();
                else
                {
                    if (!ExisteMarca())
                        CrearMarca();
                }

                this.DialogResult = DialogResult.OK;

                this.Dispose();
            }
        }


    }
}
