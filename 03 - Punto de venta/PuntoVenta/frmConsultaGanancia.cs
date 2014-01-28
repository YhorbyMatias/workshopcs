using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace PuntoVenta
{
    public partial class frmConsultaGanancia : Form
    {
        public frmConsultaGanancia()
        {
            InitializeComponent();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmConsultaGanancia_Load(object sender, EventArgs e)
        {
            
        }

        private void radioDias_CheckedChanged(object sender, EventArgs e)
        {
            dsProductos.GananciasDataTable tabla = new PuntoVenta.dsProductos.GananciasDataTable();

            //DataGridViewTextBoxColumn txt = gridGanancias.Columns[1] as DataGridViewTextBoxColumn;
            //txt.ValueType = Type.GetType("String");

            gananciasTableAdapter.FillDias(tabla, ConfigurationManager.AppSettings["Ganancia"].ToString());

            gridGanancias.DataSource = null;
            gridGanancias.Rows.Clear();

            foreach (DataRow fila in tabla.Rows)
            {
                gridGanancias.Rows.Add
                    (
                    fila[0],
                    fila[1].ToString()
                    );
            }

            //gridGanancias.DataSource = tabla;

            foreach (DataGridViewRow fila in gridGanancias.Rows)
            {
                MostrarEnFormatMoneda(fila.Cells[1]);
            }
        }

        private void radioMeses_CheckedChanged(object sender, EventArgs e)
        {
            dsProductos.GananciasDataTable tabla = new PuntoVenta.dsProductos.GananciasDataTable();

            //DataGridViewTextBoxColumn txt = gridGanancias.Columns[1] as DataGridViewTextBoxColumn;
            //txt.ValueType = Type.GetType("String");

            gananciasTableAdapter.FillByMeses(tabla, ConfigurationManager.AppSettings["Ganancia"].ToString());

            gridGanancias.DataSource = null;
            gridGanancias.Rows.Clear();
            foreach (DataRow fila in tabla.Rows)
            {
                gridGanancias.Rows.Add
                    (
                    fila[0],
                    fila[1].ToString()
                    );
            }
            //gridGanancias.DataSource = tabla;

            foreach (DataGridViewRow fila in gridGanancias.Rows)
            {
                ArreglarMes(fila.Cells[0]);
                MostrarEnFormatMoneda(fila.Cells[1]);
            }
            
        }

        void MostrarEnFormatMoneda(DataGridViewCell celda)
        {
            StringBuilder sb = new StringBuilder();
            Decimal valor = Convert.ToDecimal(celda.Value);
            sb.Append(string.Format("{0:C}", valor));

            celda.Value = sb.ToString();

            //celda.Value = string.Format("{0:C}", celda.Value); 
        }
        void ArreglarMes(DataGridViewCell celda)
        {
            switch (celda.Value.ToString())
            {
                case "1": celda.Value = "Enero";
                    break;
                case "2": celda.Value = "Febrero";
                    break;
                case "3": celda.Value = "Marzo";
                    break;
                case "4": celda.Value = "Abril";
                    break;
                case "5": celda.Value = "Mayo";
                    break;
                case "6": celda.Value = "Junio";
                    break;
                case "7": celda.Value = "Julio";
                    break;
                case "8": celda.Value = "Agosto";
                    break;
                case "9": celda.Value = "Septiembre";
                    break;
                case "10": celda.Value = "Octubre";
                    break;
                case "11": celda.Value = "Noviembre";
                    break;
                case "12": celda.Value = "Diciembre";
                    break;
                default:
                    break;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            dsProductos.GananciasDataTable tabla = new PuntoVenta.dsProductos.GananciasDataTable();

            //DataGridViewTextBoxColumn txt = gridGanancias.Columns[1] as DataGridViewTextBoxColumn;
            //txt.ValueType = Type.GetType("String");

            gananciasTableAdapter.FillByFechaExacta(tabla, ConfigurationManager.AppSettings["Ganancia"].ToString(),dateTimePicker1.Value.ToShortDateString());

            gridGanancias.DataSource = null;
            gridGanancias.Rows.Clear();

            foreach (DataRow fila in tabla.Rows)
            {
                gridGanancias.Rows.Add
                    (
                    fila[0],
                    fila[1].ToString()
                    );
            }

            //gridGanancias.DataSource = tabla;

            foreach (DataGridViewRow fila in gridGanancias.Rows)
            {
                MostrarEnFormatMoneda(fila.Cells[1]);
            }
        }
    }
}
