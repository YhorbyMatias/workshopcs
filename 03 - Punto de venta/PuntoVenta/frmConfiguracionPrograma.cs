using System;
using System.Windows.Forms;
using System.Configuration;
using PuntoVenta.Properties;
using System.Text.RegularExpressions;
/**
 * MATERIA: PROYECTO III
 * */
namespace PuntoVenta
{
    public partial class frmConfiguracionPrograma : Form
    {
        public frmConfiguracionPrograma()
        {
            InitializeComponent();
        }

        private void frmConfiguracionPrograma_Load(object sender, EventArgs e)
        {
            try
            {
                Configuration configuracion = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                txtEmpresa.Text = configuracion.AppSettings.Settings["Empresa"].Value;
                txtTelefono.Text = configuracion.AppSettings.Settings["Telefono"].Value;    
                txtDireccion.Text = configuracion.AppSettings.Settings["Direccion"].Value;
                txtRNC.Text = configuracion.AppSettings.Settings["RNC"].Value;
                txtEmail.Text = configuracion.AppSettings.Settings["Email"].Value;
                nudImpuestos.Value = 100M*Convert.ToDecimal(configuracion.AppSettings.Settings["Impuestos"].Value);
                nudGanancia.Value = 100M*Convert.ToDecimal(configuracion.AppSettings.Settings["Ganancia"].Value);
                configuracion = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message,"Excepción",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //Comprobar();
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Empresa"].Value = txtEmpresa.Text;
                config.AppSettings.Settings["Telefono"].Value = txtTelefono.Text;
                config.AppSettings.Settings["Direccion"].Value = txtDireccion.Text;
                config.AppSettings.Settings["RNC"].Value = txtRNC.Text;
                config.AppSettings.Settings["Email"].Value = txtRNC.Text;
                config.AppSettings.Settings["Impuestos"].Value = nudImpuestos.Value.ToString();
                config.AppSettings.Settings["Ganancia"].Value = nudImpuestos.Value.ToString();
                config.Save(ConfigurationSaveMode.Modified);
                config = null;
                MessageBox.Show("Hecho");
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void Comprobar()
        {
            //if (!Regex.Match(txtEmail.Text, @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$ ").Success)
            //{
            //    throw new Exception("Email inválido");
            //}
        }
    }
}
